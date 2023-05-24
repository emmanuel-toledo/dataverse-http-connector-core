using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics.Crm.Http.Connector.Core.Utilities
{
    internal static class Check
    {
        public static T NotNull<T>(T value, string parameterName)
        {
            if(value is null)
            {
                NotNull(parameterName, nameof(parameterName));
                throw new ArgumentNullException(nameof(parameterName));
            }
            return value;
        }
    }
}
