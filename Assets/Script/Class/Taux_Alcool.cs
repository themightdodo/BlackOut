using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taux_Alcool {

    public float Taux;
    public float MidTaux;
    public float MaxTaux;

    public Taux_Alcool(float MaxValue = 30)
    {
        Taux = 0;
        MidTaux = MaxValue / 3;
        MaxTaux = MaxValue;
    }
    public void add(float value)
    {
        Taux += value;
    }
    public bool Mid()
    {
        if (Taux > MidTaux)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool ToMuch()
    {
        if(Taux > MaxTaux)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
