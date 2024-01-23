using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private float speed;
    private int health;
    private int damage;
    private float range;
    public LayerMask plantMask;
    private float eatCooldown;
    private bool canEat = true;
    public Plant targetPlant;
    public ZombieType type;

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
        Invoke("ResetEatCooldown", eatCooldown);

        targetPlant.Hit(damage);
    }

    private void ResetEatCooldown()
    {
        canEat = true;
    }

    private void FixedUpdate()
    {
        if (!targetPlant)
            transform.position -= new Vector3(speed, 0 ,0);
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
