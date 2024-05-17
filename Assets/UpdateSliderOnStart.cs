using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UpdateSliderOnStart : MonoBehaviour
{
    public string Parameter;
    public AudioMixer BeuverieMaster;
    public AudioMixer InvestigationMaster;

    private void Start()
    {
        float value;
        InvestigationMaster.GetFloat(Parameter, out value);
        GetComponent<Slider>().value = value;
    }

}
