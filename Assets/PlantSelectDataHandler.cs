using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSelectDataHandler : MonoBehaviour
{
    public static PlantSelectDataHandler Instance;
    public GameObject[] PlantSelections;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
