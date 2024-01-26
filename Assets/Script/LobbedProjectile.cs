using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.Interfaces;
using UnityEngine;

public class LobbedProjectile : MonoBehaviour, IProjectile
{
    public int damage;
    public int framesUntilHit = 300;
    public GameObject target;
    public Vector3 initialLocation;
    [SerializeField] private AudioClip _onHitClip;

    private int frame = 0;
    private void Start()
    {
    }

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Zombie>(out Zombie zombie))
        {
            zombie.Hit(damage);
            SoundManager.Instance.PlaySound(_onHitClip);
            Destroy(gameObject);
        }
    }

    private bool TryHitTarget()
    {
        if (target == null)
        {
            return false;
        }
        else
        {
            var zombie = target.GetComponent<Zombie>();
            zombie.Hit(damage);
            SoundManager.Instance.PlaySound(_onHitClip);
        }
        Destroy(gameObject);
        return true;
    }

    public void Move()
    {
        frame++;
        if (frame > framesUntilHit)
        {
            TryHitTarget();
            return;
        }
        var targetPosition = target.transform.position;
        var distanceToTarget = (targetPosition.x - initialLocation.x);

        var xValue = distanceToTarget / 2;
        var aValue = 3 / (xValue * xValue);

        var xTraveled = distanceToTarget / framesUntilHit * frame - distanceToTarget / 2;
        var yTraveled = -aValue * xTraveled * xTraveled;
        var newLocation = new Vector3(initialLocation.x + xTraveled + xValue, initialLocation.y + yTraveled + 3, 0);
        gameObject.transform.position = newLocation;
    }
}
