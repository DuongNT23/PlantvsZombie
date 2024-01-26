using System;
using UnityEngine;

namespace Assets.Script.Plants
{
    public class PotatoMinePlant : Plant
    {
        public float timeUntilToArm = 14;
        public float timeUntilArmed = 3;
        public int explosionDamage = 1800;
        [SerializeField] private Sprite armedSprite;
        private bool isArmed = false;

        public override void Start()
        {
            base.Start();
            Invoke(nameof(ToArm), timeUntilToArm);
        }
        public override void Hit(int damage)
        {
            if (isArmed)
            {
                Spudow();
            }
            base.Hit(damage);
        }

        private void ToArm()
        {
            Invoke(nameof(Arm),timeUntilArmed);
            health = Int32.MaxValue;
            gameObject.GetComponent<SpriteRenderer>().sprite = armedSprite;
        }

        private void Arm()
        {
            isArmed = true;
        }

        private void Spudow()
        {
            var vectorA = new Vector3(1, 1, 0);
            var vectorB = new Vector3(-1, -1, 0);
            var position = gameObject.transform.position;
            var overlapArea = Physics2D.OverlapAreaAll(position + vectorA, position + vectorB);
            Debug.Log($"Potato Mine spotted {overlapArea.Length} objects.");
            foreach (var x in overlapArea)
            {
                if (x.TryGetComponent<Zombie>(out var zombie))
                {
                    zombie.Hit(explosionDamage);
                }
            }
            Destroy(gameObject);
        }
    }
}
