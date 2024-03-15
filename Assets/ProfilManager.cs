using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfilManager : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public Image Image;
    public Character character;

    private void Start()
    {
        Text.text = character.name;
        Image.sprite = character.ProfilePicture;
    }
}
