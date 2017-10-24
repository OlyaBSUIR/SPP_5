using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Warrior : IPlayer
    {
        private readonly IWeapon weapon1;
        private readonly IMedicineChest chest;

        public Warrior(IWeapon weapon1,IMedicineChest chest)
        {
            if (weapon1 == null)
                throw new ArgumentNullException(weapon1.ToString());
            if (chest == null)
                throw new ArgumentNullException(weapon1.ToString());
            this.weapon1 = weapon1;
            this.chest = chest;

        }

        public string Attack()
        {
            return weapon1.Strike() + "  " + chest.Cure();
            
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

            return (this.weapon1.Equals(p.weapon1));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
