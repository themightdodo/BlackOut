using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Drink : ScriptableObject
{
    public float TauxAlcoolPlus;
    public float TimeToDrink;

    public enum Type
    {
        BLUE,
        YELLOW,
        RED,
    }
    public Type type_;
}
