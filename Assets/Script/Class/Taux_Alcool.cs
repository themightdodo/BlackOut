using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taux_Alcool {

    public float Taux;
    public float MidTaux;
    public float MaxTaux;

    public int MaxBoisson;
    public int Boisson_bleu;
    public int Boisson_Jaune;
    public int Boisson_Rouge;

    public Taux_Alcool(float MaxValue = 30, int maxBoisson = 7)
    {
        Taux = 0;
        MidTaux = MaxValue / 3;
        MaxTaux = MaxValue;
        MaxBoisson = maxBoisson;
    }
    public void add(float value, Drink.Type type,int quantity)
    {
        switch (type)
        {
            case Drink.Type.BLUE:
                Boisson_bleu += quantity;
                break;
            case Drink.Type.YELLOW:
                Boisson_Jaune += quantity;
                break;
            case Drink.Type.RED:
                Boisson_Rouge += quantity;
                break;
        }
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
    public int BoissonTaken()
    {
        return Boisson_bleu + Boisson_Jaune + Boisson_Rouge;
    }
    public bool ToMuch()
    {
        if(BoissonTaken() > MaxBoisson)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
