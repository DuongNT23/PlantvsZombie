using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public Transform[] spwanPoints;

    public GameObject zombie;

    private void Start()
    {
        SpawnZombie();
    }
    void SpawnZombie()
    {
        int r = Random.Range(0, spwanPoints.Length) ; 
        GameObject myZombie = Instantiate(zombie, spwanPoints[r].position, Quaternion.identity);
    }
}
