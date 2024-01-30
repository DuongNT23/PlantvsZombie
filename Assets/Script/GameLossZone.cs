using System.Collections;
using System.Collections.Generic;
using Assets.Script.Zombies;
using UnityEngine;

public class GameLossZone : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something entered this zone.");
        if (other.gameObject.TryGetComponent<AbstractZombie>(out var zombie))
        {
            Debug.Log("You lose!");
        }
    }
}
