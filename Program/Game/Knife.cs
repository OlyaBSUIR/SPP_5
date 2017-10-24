using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Knife : IWeapon
    {
        public string Strike()
        {
            return "Удар ножом";
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Knife p = obj as Knife;
            if ((System.Object)p == null)
            {
                return false;
            }

            return (this.ToString() == p.ToString());
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
