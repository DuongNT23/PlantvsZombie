using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.Interfaces;
using UnityEngine;

public class Mower : MonoBehaviour
{
    public float speed = .4f;
    private bool activated = false;

    private void Start()
    {
        //Destroy(gameObject,10);
    }
    private void Update()
    {
        if (activated)
        {
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Zombie>(out Zombie zombie)){
            Destroy(zombie.gameObject);
            if (!activated)
            {
                activated = true;
                Invoke(nameof(Remove),8);
            }
        }    
    }

    private void Remove()
    {
        Destroy(gameObject);
    }

    public void Move()
    {
        transform.position += new Vector3(speed * Time.fixedDeltaTime, 0, 0);
    }
}
