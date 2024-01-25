using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{
    public int damage;
    public float speed = .8f;

    [SerializeField] protected AudioClip _onPeaHitClip;

    private void Start()
    {
        //Destroy(gameObject,10);
    }
    private void Update()
    {
        transform.position += new Vector3(speed * Time.fixedDeltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Zombie>(out Zombie zombie)){
            zombie.Hit(damage);
            SoundManager.Instance.PlaySound(_onPeaHitClip);
            Destroy(gameObject);
        }    
    }
}
