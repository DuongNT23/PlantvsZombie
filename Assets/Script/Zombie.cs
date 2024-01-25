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

    private static readonly Color chilledColor = new Color(0.22f,0.42f,0.95f);

    //Unique boolean for freezing/chilling
    private bool isChilled = false;
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

    private float GetFinalSpeed()
    {
        float finalSpeed = speed;
        if (isChilled)
        {
            finalSpeed /= 2;
        }
        return finalSpeed;
    }

    private float GetFinalEatCooldown()
    {
        float finalEatCooldown = eatCooldown;
        if (isChilled)
        {
            finalEatCooldown *= 2;
        }
        return finalEatCooldown;
    }

    public void Chill()
    {
        CancelInvoke(nameof(Unchill));
        if (!isChilled)
        {
            isChilled = true;
            GetComponent<SpriteRenderer>().color = chilledColor;
        }
        Invoke(nameof(Unchill),10);
    }

    public void Unchill()
    {
        if (isChilled)
        {
            isChilled = false;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
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
