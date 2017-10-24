using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Gun : IWeapon
    {
        public string Strike()
        {
            return "Выстрел!!!";
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Gun p = obj as Gun;
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
