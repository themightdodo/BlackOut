using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanManager : MonoBehaviour
{
  public static  OceanManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(Instance);
        }
    }
   
    public float wavesHeight = 0.28f;

    public float wavesFrequency = 0.15f;

    public float wavesSpeed = 0.03f;

    public Transform Ocean;

    Material OceanMat;
    Texture2D wavesDisplacement;

    // Start is called before the first frame update
    void Start()
    {
        SetVariables();
    }

    // On récup & on assigne les variables du shader au script
    void SetVariables()
    {
        OceanMat = Ocean.GetComponent<Renderer>().sharedMaterial;
        wavesDisplacement = (Texture2D)OceanMat.GetTexture("_WavesDisplacement");
    }

    /// <summary>
    /// en gros on fait le tilling des UV, sachant que le tilling revient à multiplier les UVs( le U & le V)
    /// On reprend le code du hader mais en script
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>&
    public float WaterHeightAtPosition(Vector3 position)
    {
        return Ocean.position.y + wavesDisplacement.GetPixelBilinear(position.x * wavesFrequency, position.z * wavesFrequency * Time.deltaTime * wavesSpeed/100).g * wavesHeight/100 *Ocean.localScale.x;
    }

    /// <summary>
    /// appelé chaque fois qu'on change qq'ue chose dans l'inspecteur
    /// </summary>
    private void OnValidate()
    {
        if (!OceanMat)
        {
            SetVariables() ;
        }
            UpdateMaterial();
    }

    public void UpdateMaterial()
    {
        OceanMat.SetFloat("_WavesFrequency", wavesFrequency/ 100);
        OceanMat.SetFloat("_WavesSpeed", wavesSpeed/100);
        OceanMat.SetFloat("_WavesHeight", wavesHeight / 100);
    }

    
}
