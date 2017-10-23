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
    }
}
