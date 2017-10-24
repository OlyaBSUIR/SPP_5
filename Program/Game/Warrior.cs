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

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Warrior p = obj as Warrior;
            if ((System.Object)p == null)
            {
                return false;
            }

            return (this.weapon.Equals(p.weapon));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
