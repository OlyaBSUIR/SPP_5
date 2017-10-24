using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Samurai : IPlayer
    {
        private IWeapon weapon;

        public Samurai()
        {

        }

        public void SetWeapon(IWeapon weapon)
        {
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

            Samurai p = obj as Samurai;
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
