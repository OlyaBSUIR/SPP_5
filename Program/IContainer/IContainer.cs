using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    public interface IContainer
    {
        void RegisterType<T>(bool isSingleton = false);
        void Register<I, T>(bool isSingleton = false);
        T Resolve<T>();
    }
}
