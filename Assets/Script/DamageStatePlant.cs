using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageStatePlant : Plant
{
    [SerializeField] private Sprite firstDamage;
    [SerializeField] private Sprite secondDamage;
    private int beginHealth;

    public override void Start()
    {
        base.Start();
        beginHealth = health;
    }

    public override void Hit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
           Destroy(gameObject);
        }
        else
        {
            UpdateSprite();
        }
    }

    private void UpdateSprite()
    {
        if (health / beginHealth <= 1 / 3)
        {
            GetComponent<SpriteRenderer>().sprite = secondDamage;
        } else if (health / beginHealth <= 2 / 3)
        {
            GetComponent<SpriteRenderer>().sprite = firstDamage;
        }
    }
}
