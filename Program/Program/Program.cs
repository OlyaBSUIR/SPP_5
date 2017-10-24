using Game;
using IOC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Program
    {
        public static void DynamicLoad()
        {
            string path = Directory.GetCurrentDirectory();
            try
            {
                IContainer dll = LoadDll(path);
                dll.Register<IWeapon, Knife>();
                dll.Register<IWeapon, Gun>();
                dll.Register<IPlayer, Warrior>();
                dll.Register<IPlayer, Samurai>(true);
                dll.RegisterType<Warrior>(true);
                dll.RegisterType<Samurai>(true);
                var warrior = dll.Resolve<Warrior>();
                warrior = dll.Resolve<Warrior>();
                Console.WriteLine(warrior.Attack());

                var samurai = dll.Resolve<Samurai>();
                samurai.SetWeapon(dll.Resolve<IWeapon>());
                Console.WriteLine(samurai.Attack());
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        public static void StaticLoad()
        {

            var con = new IOCContainer();
            con.Register<IWeapon, Knife>();
            con.Register<IWeapon, Gun>();
            con.Register<IPlayer, Warrior>();
            con.RegisterType<Warrior>(true);
            con.Register<IPlayer, Samurai>(true);
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

        public static void DynamicLoadWithReflection()
        {
            const string ASSEMBLY = "IOC";
            const string FULL_NAME_OF_TYPE = "IOC.IOCContainer";

            Assembly ass;

            try
            {
                ass = Assembly.Load(ASSEMBLY);
            }
            catch
            {
                Console.WriteLine("Не получилось подключить cборку");
                Console.ReadLine();
                return;
            }

            Type myClass;
            try
            {
                myClass = ass.GetType(FULL_NAME_OF_TYPE);
                if (myClass == null)
                    throw new Exception(" '" + FULL_NAME_OF_TYPE + "' не найден");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                return;
            }

            object instance = Activator.CreateInstance(myClass);//создание объекта нового класса

            MethodInfo[] M = myClass.GetMethods();

            if (M.Length < 5)
                return;

            Type[] paramaters = { typeof(IWeapon), typeof(Knife)};
            MethodInfo mi = myClass.GetMethod("Register");
            MethodInfo genericMI = mi.MakeGenericMethod(paramaters);
            object[] param = { true };
            genericMI.Invoke(instance, param);

            paramaters[0] = typeof(IWeapon);
            paramaters[1] = typeof(Gun);
            genericMI = mi.MakeGenericMethod(paramaters);
            param[0] =  true;
            genericMI.Invoke(instance, param);

            paramaters[0] = typeof(IPlayer);
            paramaters[1] = typeof(Warrior);
            genericMI = mi.MakeGenericMethod(paramaters);
            param[0] = false;
            genericMI.Invoke(instance, param);

            mi = myClass.GetMethod("RegisterType");
            paramaters = new Type[1];
            paramaters[0] = typeof(Warrior);
            genericMI = mi.MakeGenericMethod(paramaters);
            param[0] = false;
            genericMI.Invoke(instance, param);

            mi = myClass.GetMethod("Resolve");
            paramaters[0] = typeof(Warrior);
            genericMI = mi.MakeGenericMethod(paramaters);
            var warrior = (Warrior)genericMI.Invoke(instance, null);

            Console.WriteLine(warrior.Attack());

            Console.ReadLine();
        }

        private static IContainer LoadDll(string path)
        {
            IContainer plugin = null;
            string[] files = Directory.GetFiles(path, "*.dll");

            foreach (string file in files)
                try
                {
                    string fname = Path.GetFileNameWithoutExtension(file);
                    string dir = Directory.GetParent(file).ToString();

                    Assembly assembly = Assembly.LoadFile(file);
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (type.GetInterfaces().Contains(typeof(IContainer)))
                        {
                            plugin = (IContainer)Activator.CreateInstance(type);
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Не получилось загрузить:(");
                }
            if (plugin == null)
            {
                throw new DllNotFoundException("Не найдена такая dll:(");
            }
            return plugin;
        }

        static void Main(string[] args)
        {
            DynamicLoadWithReflection();



        }

    }
}

