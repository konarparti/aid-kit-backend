using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidKit.Core.Exceptions
{
    public class ServiceConfigException : Exception
    {
        public ServiceConfigException(string message) : base(message) { }
    }
}
