using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.Interfaces;
using UnityEngine;

public class PeaBullet : MonoBehaviour, IProjectile
{
    public int damage;
    public float speed = .8f;
    private bool hasCollided = false;

    [SerializeField] protected AudioClip _onPeaHitClip;

    private void Start()
    {
        //Destroy(gameObject,10);
    }
    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasCollided)
        {
            return;
        }
        if (other.TryGetComponent<Zombie>(out Zombie zombie)){
            hasCollided = true;
            zombie.Hit(damage);
            SoundManager.Instance.PlaySound(_onPeaHitClip);
            Destroy(gameObject);
        }    
    }

    public void Move()
    {
        transform.position += new Vector3(speed * Time.fixedDeltaTime, 0, 0);
    }
}
