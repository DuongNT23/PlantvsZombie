using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.Zombies.Accessories;
using UnityEngine;

public class ZombieFactory : MonoBehaviour
{
    [SerializeField] private Zombie basicZombie;
    [SerializeField] private Accessory cone;
    [SerializeField] private Accessory bucket;
    [SerializeField] private Accessory screenDoor;
    private ZombieFactory(){}

    private Dictionary<string, Func<Vector3, Zombie>> functionDictionary =
        new();

    private void Start()
    {
        functionDictionary.Add("normal",InstantiateBasicZombie);
        functionDictionary.Add("conehead", InstantiateConeheadZombie);
        functionDictionary.Add("buckethead", InstantiateBucketheadZombie);
        functionDictionary.Add("door",InstantiateScreenDoorZombie);
    }

    public static ZombieFactory Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Zombie InstantiateBasicZombie(Vector3 location)
    {
        return Instantiate(basicZombie,location,Quaternion.identity);
    }

    public Zombie InstantiateConeheadZombie(Vector3 location)
    {
        Debug.Log("Requested conehead");
        var zombie = InstantiateBasicZombie(location);
        zombie.SetAccessory(cone);
        return zombie;
    }

    public Zombie InstantiateBucketheadZombie(Vector3 location)
    {
        var zombie = InstantiateBasicZombie(location);
        zombie.SetAccessory(bucket);
        return zombie;
    }

    public Zombie InstantiateFromType(Vector3 location, string type)
    {
        if (functionDictionary.TryGetValue(type, out var func))
        {
            return func(location);
        }
        Debug.LogWarning($"Unknown zombie type: {type}. Spawned Basic.");
        return InstantiateBasicZombie(location);
    }

    public Zombie InstantiateScreenDoorZombie(Vector3 location)
    {
        var zombie = InstantiateBasicZombie(location);
        zombie.SetAccessory(screenDoor);
        return zombie;
    }
}
