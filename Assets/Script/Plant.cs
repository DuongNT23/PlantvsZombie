using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int health;
    [SerializeField] private AudioClip eatenAudioClip;
    public virtual void Start()
    {
        gameObject.layer = 9;
    }
    public virtual void Hit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            SoundManager.Instance.PlaySound(eatenAudioClip);
           Destroy(gameObject);
        }
    }
}
