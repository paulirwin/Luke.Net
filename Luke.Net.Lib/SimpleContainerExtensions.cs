using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luke.Net
{
    public static class SimpleContainerExtensions
    {
        public static T GetInstance<T>(this SimpleContainer container)
        {
            return (T)container.GetInstance(typeof(T), null);
        }
    }
}
