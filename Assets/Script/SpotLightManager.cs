using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightManager : Beuverie_Character_StateMachine
{
    Light spot;
    public float StartSpot;
    public float ChangeMultiplier;
    public int MaxSpot = 179;

    protected override void Start()
    {
        base.Start();
        spot = GetComponent<Light>();
    }
    protected override void Update()
    {
        spot.spotAngle = ((StartSpot - (pm.Taux*ChangeMultiplier))* MaxSpot) / StartSpot;
        spot.innerSpotAngle = ((StartSpot - (pm.Taux * ChangeMultiplier))* MaxSpot) / StartSpot;
    }
}
