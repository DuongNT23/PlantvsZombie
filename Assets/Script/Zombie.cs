using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.Zombies;
using UnityEngine;

public class Zombie : AbstractZombie
{
    private void Start()
    {
        health = type.health;
        damage = type.damage;
        range = type.range; 
        speed = type.speed;
        eatCooldown = type.eatCooldown;
        GetComponent<SpriteRenderer>().sprite = type.sprite;
    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, range, plantMask);

        if (hit.collider)
        {
            targetPlant = hit.collider.GetComponent<Plant>();
            Eat();
        }

        if(health == 2)
        {
            GetComponent<SpriteRenderer>().sprite = type.deathSprite;
        }
    }

    void Eat()
    {
        if(!canEat || !targetPlant)
        {
            return;
        }
        canEat = false;
        Invoke("ResetEatCooldown", GetFinalEatCooldown());

        targetPlant.Hit(damage);
    }

    private void ResetEatCooldown()
    {
        canEat = true;
    }

    private void FixedUpdate()
    {
        if (!targetPlant)
            transform.position -= new Vector3(GetFinalSpeed(), 0 ,0);
    }

    public void Hit(int damage)
    {
        health -= damage;
        if (health <= 0 )
        {
            Destroy(gameObject);
        }
    }
}
