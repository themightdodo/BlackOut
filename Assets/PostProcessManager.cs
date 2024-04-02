using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessManager : MonoBehaviour
{
    public FullScreenPassRendererFeature FPR;
    public float PostProcessAmount;
    // Start is called before the first frame update
    void Start()
    {
        FPR.passMaterial.SetFloat("_Amount", PostProcessAmount);
    }
}
