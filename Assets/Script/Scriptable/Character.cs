using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Character : ScriptableObject
{
    public string name;
    public Sprite ProfilePicture;
    [TextArea(10,3)]
    public string Desc;
}
