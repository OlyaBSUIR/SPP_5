using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Warrior : IPlayer
    {
        private readonly IWeapon weapon;

        public Warrior(IWeapon weapon)
        {
            if (weapon == null)
                throw new ArgumentNullException(weapon.ToString());
            this.weapon = weapon;
        }

        public string Attack()
        {
            return weapon.Strike();
        }
    }
}
