using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessManager : MonoBehaviour
{
    public FullScreenPassRendererFeature FPR;
    public float PostProcessAmount;
    public Color posterisationColor;
    public float BasePostProcessAmount { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        FPR.passMaterial.SetFloat("_Amount", PostProcessAmount);
        FPR.passMaterial.SetColor("_Color",posterisationColor);
        BasePostProcessAmount = PostProcessAmount;
    }
}
