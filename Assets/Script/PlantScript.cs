using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantScript : AbstractPlantScript
{
    public Sprite plantSprite;

    public GameObject plantObject;

    public int price;

    public Image icon;

    public TextMeshProUGUI priceText;

    private GameManage gms;

    [SerializeField] private int cooldown;
    

    public override void Start()
    {
        //TODO Cooldown
        gms = GameObject.Find("GameManage").GetComponent<GameManage>();
        images = gameObject.GetComponentsInChildren<Image>();
        GetComponent<Button>().onClick.AddListener(BuyPlant);
    }

    private void Update()
    {
        if (gms.suns >= price)
        {
            Enable();
        }
        else
        {
            Disable();
        }
    }

    

    private void BuyPlant()
    {
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
