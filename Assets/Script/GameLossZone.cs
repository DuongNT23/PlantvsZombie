using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.Zombies;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLossZone : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Something entered this zone.");
        if (other.TryGetComponent<Zombie>(out Zombie zombie))
        {
            Debug.Log("You lose!");
            Destroy(zombie.gameObject);
            SceneManager.LoadSceneAsync(0);
        }
    }

}
