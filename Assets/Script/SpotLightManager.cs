using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightManager : Beuverie_Character_StateMachine
{
    Light spot;
    public float StartSpot;
    public float ChangeMultiplier;
    public int MaxSpot = 179;
    public int MinSpot = 16;

    protected override void Start()
    {
        base.Start();
        spot = GetComponent<Light>();
    }
    protected override void Update()
    {
        if(spot.spotAngle >= MinSpot)
        {
            spot.spotAngle = ((StartSpot - (pm.Taux * ChangeMultiplier)) * MaxSpot) / StartSpot;
            spot.innerSpotAngle = ((StartSpot - (pm.Taux * ChangeMultiplier)) * MaxSpot) / StartSpot;
        }
        else
        {
            spot.spotAngle = MinSpot;
            spot.innerSpotAngle = MinSpot;
        }

    }
}
