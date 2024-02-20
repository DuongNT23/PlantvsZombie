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

    private Image[] images;

    private void Start()
    {
        //TODO Cooldown
        gms = GameObject.Find("GameManage").GetComponent<GameManage>();
        GetComponent<Button>().onClick.AddListener(BuyPlant);
        images = gameObject.GetComponentsInChildren<Image>();
    }

    private void Update()
    {
        if (gms.suns >= price)
        {
            foreach (var image in images)
            {
                image.color = Color.white;
            }
        }
        else
        {
            foreach (var image in images)
            {
                image.color = Color.gray;
            }
        }
    }

    private void BuyPlant()
    {
        //TODO Grey out if cannot buy
        if (gms.suns >=  price)
        {
            gms.BuyPlant(plantObject, plantSprite, price);
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
