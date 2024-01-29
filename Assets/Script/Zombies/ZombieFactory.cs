using System.Collections;
using System.Collections.Generic;
using Assets.Script.Zombies.Accessories;
using UnityEngine;

public class ZombieFactory : MonoBehaviour
{
    [SerializeField] private Zombie basicZombie;
    [SerializeField] private Accessory cone;
    [SerializeField] private Accessory bucket;
    private ZombieFactory(){}

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

}
