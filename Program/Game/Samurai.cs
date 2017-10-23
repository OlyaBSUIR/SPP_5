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
    }
}
