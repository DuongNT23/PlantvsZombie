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
        private int health;
        public bool isMetal = false;

        /// <summary>
        /// Inflict damage to the accessory.
        /// </summary>
        /// <param name="damage">Amount of damage</param>
        /// <returns>How much damage was not inflicted to the accessory.</returns>
        public virtual int Hit(int damage)
        {
            if (health < damage)
            {
                var remain = damage - health;
                health = 0;
                return remain;
            }
            health -= damage;
            return 0;
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
