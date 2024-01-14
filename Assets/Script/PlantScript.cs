using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantScript : MonoBehaviour
{
    public Sprite plantSprite;

    public GameObject plantObject;

    public int price;

    public Image icon;

    public TextMeshProUGUI priceText;

    private GameManage gms;

    private void Start()
    {
        gms = GameObject.Find("GameManage").GetComponent<GameManage>();
        GetComponent<Button>().onClick.AddListener(BuyPlant);
    }

    private void BuyPlant()
    {
        if (gms.suns >=  price && !gms.currentPlant)
        {
            gms.suns -= price;
            gms.BuyPlant(plantObject, plantSprite);

        }
    }

    private void OnValidate()
    {
        if (plantSprite)
        {
            icon.enabled = true;
            icon.sprite = plantSprite;
            priceText.text = price.ToString();
        }
        else
        {
            icon.enabled = false;
        }
    }
}
