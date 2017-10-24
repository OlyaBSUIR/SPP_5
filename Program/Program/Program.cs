using Game;
using IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            var con = new IOCContainer();
            con.RegisterType<IWeapon, Knife>();
            con.RegisterType<IWeapon, Gun>();
            con.RegisterType<IPlayer, Warrior>();
            con.RegisterType<Warrior>(true);
            con.RegisterType<IPlayer, Samurai>(true);
            con.RegisterType<Samurai>(true);

            //con.RegisterType<IWeapon>();
            var warrior = con.Resolve<Warrior>();
            warrior = con.Resolve<Warrior>();
            Console.WriteLine(warrior.Attack());

            var samurai = con.Resolve<Samurai>();
            samurai.SetWeapon(con.Resolve<IWeapon>());
            Console.WriteLine(samurai.Attack());
            Console.ReadLine();
        }
    }
}
