using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ZombieType", menuName = "Zombie")]
public class ZombieType : ScriptableObject
{
    public int health;

    public int damage;

    public float range = .5f;

    public float eatCooldown = 1f;

    public float speed;

    public Sprite sprite;

    public Sprite deathSprite;
}
