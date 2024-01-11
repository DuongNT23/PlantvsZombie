using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantScript : MonoBehaviour
{
    public Sprite plantSprite;

    public GameObject plantObject;

    public Image icon;

    private void OnValidate()
    {
        if (plantSprite)
        {
            icon.enabled = true;
            icon.sprite = plantSprite;
        }
        else
        {
            icon.enabled = false;
        }
    }
}
