using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics.Crm.Http.Connector.Core.Domains.Enums
{
    public enum ConditionTypes
    {
        Equal, // string, int, double, guid, datetime.
        NotEqual, // string, int, double, guid, datetime.
        In, // string, int, double, guid, datetime.
        NotIn, // string, int, double, guid, datetime.
        Null, // string, int, double, guid, datetime.
        NotNull, // string, int, double, guid, datetime.
        BeginsWith, // string, int, double, guid, datetime.
        DoesNotBeginWith, // string, int, double, guid, datetime.
        EndsWith, // string, int, double, guid, datetime.
        DoesNotEndsWith, // string, int, double, guid, datetime.
        Like,// string, int, double, guid, datetime.
        NotLike, // string, int, double, guid, datetime.
        Between, // int (set initial value and end value, just two values)
        NotBetween, // int (set initial value and end value)
        GreaterThan, // int
        GreaterEqual, // int
        LessThan, // int
        LessEqual // int
    }
}
