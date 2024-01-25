using UnityEngine;

namespace Assets.Script
{
    public class SnowPeaBullet : PeaBullet
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Zombie>(out Zombie zombie))
            {
                zombie.Hit(damage);
                zombie.Chill();
                SoundManager.Instance.PlaySound(_onPeaHitClip);
                Destroy(gameObject);
            }
        }
    }
}