using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Zombies.Accessories
{
    public class Accessory : MonoBehaviour
    {
        public int health;
        public bool isMetal = false;


        [SerializeField] private Sprite firstDamagedSprite;
        [SerializeField] private Sprite secondDamagedSprite;

        [SerializeField] private int healthUntilFirstDamaged;
        [SerializeField] private int healthUntilSecondDamaged;
        [SerializeField] public Vector3 dislodgeSprite;

        private SpriteRenderer renderer;

        private void Start()
        {
            renderer = gameObject.GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// Inflict damage to the accessory.
        /// </summary>
        /// <param name="damage">Amount of damage</param>
        /// <returns>How much damage was not inflicted to the accessory.</returns>
        public virtual int Hit(int damage, int damageType = DamageType.DIRECT)
        {
            int remain = 0;
            if (health < damage)
            {
                remain = damage - health;
                health = 0;
            }
            else
            {
                health -= damage;
            }

            UpdateSprite();
            return remain;
        }

        private void UpdateSprite()
        {
            if (health <= healthUntilSecondDamaged)
            {
                renderer.sprite = secondDamagedSprite;
            } else if (health <= healthUntilFirstDamaged)
            {
                renderer.sprite = firstDamagedSprite;
            }
        }


        public void RemoveAccessory(Zombie owner)
        {
            if (this != owner.accessory)
            {
                Debug.LogError("A zombie attempted to remove an accessory of another zombie.");
                return;
            }
            owner.accessory = null;
            OnAccessoryRemoved(owner);
            Destroy(gameObject);
        }

        protected virtual void OnAccessoryRemoved(Zombie owner)
        {

        }

        public bool isDead()
        {
            return health <= 0;
        }
    }
}
