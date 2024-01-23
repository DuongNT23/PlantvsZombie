using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShooter : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootOrigin;
    public float cooldown;
    private bool canShoot;
    public float range;
    public LayerMask shootMask;
    private GameObject target;

    [SerializeField] private AudioClip _shootClip;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, range, shootMask);
        if (hit.collider)
        {
            target = hit.collider.gameObject;
            Shoot();
        }
    }

    private void Start()
    {
        Invoke("ResetCooldown", cooldown);
    }

    private void ResetCooldown()
    {
        canShoot = true;
    }

    void Shoot()
    {
        if(!canShoot)
        {
            return;
        }
        canShoot = false;
        Invoke("ResetCooldown", cooldown);

        GameObject myBullet = Instantiate(bullet, shootOrigin.position, Quaternion.identity);
        SoundManager.Instance.PlaySound(_shootClip);
    }
}
