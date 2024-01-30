﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Zombies.Accessories
{
    public class ShieldAccessory : Accessory
    {
        public override int Hit(int damage, int damageType)
        {
            if (damageType == DamageType.DIRECT)
            {
                return base.Hit(damage, damageType);
            }
            return damage;
        }
    }
}
