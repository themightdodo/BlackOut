void AdditionalLights_half(float3 SpecColor, float Smoothness, float3 WorldPosition, float3 WorldNormal, float3 WorldView, half4 Shadowmask,
	out float3 Diffuse, out float3 Specular) {
	float3 diffuseColor = 0;
	float3 specularColor = 0;
#ifndef SHADERGRAPH_PREVIEW
	Smoothness = exp2(10 * Smoothness + 1);
	uint pixelLightCount = GetAdditionalLightsCount();
	uint meshRenderingLayers = GetMeshRenderingLayer();

#if USE_FORWARD_PLUS
	for (uint lightIndex = 0; lightIndex < min(URP_FP_DIRECTIONAL_LIGHTS_COUNT, MAX_VISIBLE_LIGHTS); lightIndex++) {
		FORWARD_PLUS_SUBTRACTIVE_LIGHT_CHECK
			Light light = GetAdditionalLight(lightIndex, WorldPosition, Shadowmask);
#ifdef _LIGHT_LAYERS
		if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
#endif
		{
			// Blinn-Phong
			float3 attenuatedLightColor = light.color * (light.distanceAttenuation * light.shadowAttenuation);
			diffuseColor += LightingLambert(attenuatedLightColor, light.direction, WorldNormal);
			specularColor += LightingSpecular(attenuatedLightColor, light.direction, WorldNormal, WorldView, float4(SpecColor, 0), Smoothness);
		}
	}
#endif

	// For Foward+ the LIGHT_LOOP_BEGIN macro will use inputData.normalizedScreenSpaceUV, inputData.positionWS, so create that:
	InputData inputData = (InputData)0;
	float4 screenPos = ComputeScreenPos(TransformWorldToHClip(WorldPosition));
	inputData.normalizedScreenSpaceUV = screenPos.xy / screenPos.w;
	inputData.positionWS = WorldPosition;

	LIGHT_LOOP_BEGIN(pixelLightCount)
		Light light = GetAdditionalLight(lightIndex, WorldPosition, Shadowmask);
#ifdef _LIGHT_LAYERS
	if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
#endif
	{
		// Blinn-Phong
		float3 attenuatedLightColor = light.color * (light.distanceAttenuation * light.shadowAttenuation);
		diffuseColor += LightingLambert(attenuatedLightColor, light.direction, WorldNormal);
		specularColor += LightingSpecular(attenuatedLightColor, light.direction, WorldNormal, WorldView, float4(SpecColor, 0), Smoothness);
	}
	LIGHT_LOOP_END
#endif

	Diffuse = diffuseColor;
	Specular = specularColor;
}

void MainLightShadows_half(float3 WorldPos, half4 Shadowmask, out float ShadowAtten) {
#ifdef SHADERGRAPH_PREVIEW
	ShadowAtten = 1;
#else
#if defined(_MAIN_LIGHT_SHADOWS_SCREEN) && !defined(_SURFACE_TYPE_TRANSPARENT)
	float4 shadowCoord = ComputeScreenPos(TransformWorldToHClip(WorldPos));
#else
	float4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
#endif
	ShadowAtten = MainLightShadow(shadowCoord, WorldPos, Shadowmask, _MainLightOcclusionProbes);
#endif
}

void MainLight_half(float3 WorldPos,out float3 Direction, out float3 Color, out half DistanceAtten, out half ShadowAtten) {
#if SHADERGRAPH_PREVIEW
	Direction = half3(0.5, 0.5, 0);
	Color = 1;
	DistanceAtten = 1;
	ShadowAtten = 1;
#else
#if SHADOWS_SCREEN
	half4 clipPos = TransformWorldToHClip(WorldPos);
	half4 shadowCoord = ComputeScreenPos(clipPos);
#else
	half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
#endif
	Light mainLight = GetMainLight(shadowCoord);
	Direction = mainLight.direction;
	Color = mainLight.color;
	DistanceAtten = mainLight.distanceAttenuation;
	ShadowAtten = mainLight.shadowAttenuation;
#endif
}

void DirectSpecular_half(half3 Specular, half Smoothness, half3 Direction, half3 Color, half3 WorldNormal, half3 WorldView, out half3 Out)
{
#if SHADERGRAPH_PREVIEW
	Out = 0;
#else
	Smoothness = exp2(10 * Smoothness + 1);
	WorldNormal = normalize(WorldNormal);
	WorldView = SafeNormalize(WorldView);
	Out = LightingSpecular(Color, Direction, WorldNormal, WorldView, half4(Specular, 0), Smoothness);
#endif
}

