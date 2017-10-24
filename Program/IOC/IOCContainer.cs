using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IOC
{
    public class IOCContainer:IContainer
    {
        private Dictionary<Type, List<Type>> typeDict;

        private Dictionary<Type, List<object>> instanceDict;

        public IOCContainer()
        {
            typeDict = new Dictionary<Type, List<Type>>();
            instanceDict = new Dictionary<Type, List<object>>();
        }

        public T Resolve<T>()
        {
            CheckTypeResolution(typeof(T));

            Type keyType = typeof(T);
            if (instanceDict.ContainsKey(keyType))
            {
                AddInstance<T>(Resolve(keyType));
                return (T)instanceDict[keyType].Last();
            }
            else
            {
                return (T)Resolve(keyType);
            }
        }

        public void RegisterType<T>(bool isSingleton = false)
        {
            AddType<T>(typeof(T));
            if (isSingleton)
            {
                AddInstance<T>(null);
            }
        }

        public void Register<I, T>(bool isSingleton = false)
        {
            AddType<I>(typeof(T));

            if (isSingleton)
            {
                AddInstance<T>(null);
            }
        }

        private void CheckTypeResolution(Type type)
        {
            if (!typeDict.ContainsKey(type))
            {
                throw new InvalidOperationException(string.Format("There is no registered type {0}", type.FullName));
            }
        }

        private void AddType<T>(Type type)
        {
            Type keyType = typeof(T);
            if (typeDict.ContainsKey(keyType))
            {
                if (!typeDict[keyType].Contains(type))
                    typeDict[keyType].Add(type);
            }
            else
            {
                typeDict.Add(keyType, new List<Type>() { type });
            }
        }

        private void AddInstance<T>(object instance)
        {
            Type keyType = typeof(T);
            if (instanceDict.ContainsKey(keyType))
            {

                if (instanceDict[keyType] == null)
                    instanceDict[keyType] = new List<object>();
                if (!instanceDict[keyType].Contains(instance))
                    instanceDict[keyType].Add(instance);
            }
            else
            {
                instanceDict.Add(keyType, new List<object>() { instance });
            }
        }

        private object Resolve(Type type)
        {
            CheckTypeResolution(type);

            List<Type> resolvedType = typeDict[type];
            return ResolveType(resolvedType.Last());
        }

        private object ResolveType(Type type)
        {
            ConstructorInfo constructor = GetConstructorMaxParams(type);
            ParameterInfo[] constructorParams = null;

            if (constructor != null)
                constructorParams = constructor.GetParameters();

            if (constructorParams == null || constructorParams.Count() == 0)
            {
                return Activator.CreateInstance(type);
            }
            else
            {
                object[] paramInstances = new object[constructorParams.Length];
                Type parameterType = null;
                for (int i = 0; i < constructorParams.Length; i++)
                {
                    parameterType = constructorParams[i].ParameterType;
                    paramInstances[i] = Resolve(parameterType.IsByRef ? parameterType.GetElementType() : parameterType);
                }

                return constructor.Invoke(paramInstances);
            }
        }

        private ConstructorInfo GetConstructorMaxParams(Type type)
        {
            int maxNumParams = 0;
            int numParams = 0;
            int indexConstructor = 0;

            ConstructorInfo[] constructors = type.GetConstructors();

            if (constructors.Length == 0)
                return null;

            for (int i = 0; i < constructors.Length; i++)
            {
                numParams = constructors[i].GetParameters().Length;
                if (numParams > maxNumParams)
                {
                    maxNumParams = numParams;
                    indexConstructor = i;
                }
            }

            return constructors[indexConstructor];
        }
    }

}

