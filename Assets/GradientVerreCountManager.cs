using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientVerreCountManager : MonoBehaviour
{
    Material[] materials;
    Material material;
    float bluevalue = 0;
    float vertvalue = 0;
    float vertvaluebuffer;
    float bluevaluebuffer;
    float value;
    float maxvalue;
    // Start is called before the first frame update
    void Start()
    {
        materials = GetComponent<Renderer>().materials;
        material = materials[0];
    }

    // Update is called once per frame
    void Update()
    {
       if(vertvaluebuffer != vertvalue)
       {
            value = 0.5f - (vertvalue*2f) + (bluevalue*2f);
            vertvaluebuffer = vertvalue; 
           
       }
       if(bluevaluebuffer != bluevalue)
        {   
            value = 0.5f - (vertvalue*2f) + (bluevalue*2f);
            bluevaluebuffer = bluevalue;
       }
        vertvalue = (Beuverie_GameManager.GM_instance.playerManager.TauxAlcool.Boisson_Jaune * 0.5f) / Beuverie_GameManager.GM_instance.playerManager.TauxAlcool.MaxBoisson;
        bluevalue = (Beuverie_GameManager.GM_instance.playerManager.TauxAlcool.Boisson_bleu * 0.5f) / Beuverie_GameManager.GM_instance.playerManager.TauxAlcool.MaxBoisson;
        maxvalue = vertvalue + bluevalue;
       material.SetFloat("_ColorValue", value);
       material.SetFloat("_Opacity", maxvalue*4f);
    }
}
