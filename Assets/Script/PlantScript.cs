using System;
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
    public TextMeshProUGUI cooldownText;

    private GameManage gms;

    [SerializeField] private float cooldown;
    private float endCooldown = 0; 

    public override void Start()
    {
        endCooldown = Time.time;
        gms = GameObject.Find("GameManage").GetComponent<GameManage>();
        images = gameObject.GetComponentsInChildren<Image>();
        GetComponent<Button>().onClick.AddListener(BuyPlant);
    }

    private void Update()
    {
        var current = Time.time;
        if (current <= endCooldown) //Is under cooldown
        {
            Disable();
            var remaining = endCooldown - current;
            cooldownText.text = remaining.ToString("0.0");
            return;
        }
        cooldownText.text = String.Empty;
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
        if (isEnabled)
        {
            gms.BuyPlant(plantObject, plantSprite, price, this);
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

    public void Bought()
    {
        endCooldown = Time.time + cooldown;
    }
}
