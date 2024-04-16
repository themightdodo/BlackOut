using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class IndiceItem : ScriptableObject
{
    public string name;
    public Sprite Image;
    [TextArea(10,3)]
    public string Desc;
    public bool NextObjective;
}
