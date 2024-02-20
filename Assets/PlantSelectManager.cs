using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Assets.Script;
using UnityEngine;

public class PlantSelectManager : MonoBehaviour
{
    [SerializeField] private Transform SpawnLocation;
    [SerializeField] private Canvas canvas;

    [SerializeField] private int plantsPerRow = 10;
    [SerializeField] private GameObject[] plantSelects;

    [SerializeField] private float xGap = 50;
    [SerializeField] private float yGap = 50;

    // Start is called before the first frame update
    void Start()
    {
    }
}
