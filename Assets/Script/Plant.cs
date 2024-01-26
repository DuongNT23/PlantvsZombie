using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int health;

    public virtual void Start()
    {
        gameObject.layer = 9;
    }
    public virtual void Hit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
           Destroy(gameObject);
        }
    }
}
