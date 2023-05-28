using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics.Crm.Http.Connector.Core.Domains.Enums
{
    public enum ConditionTypes
    {
        Equal,
        NotEqual,
        In, // Pending to create "condition" tag for query. Evaluate In condition in filterbuilder class.
        NotIn, // Pending to create "condition" tag for query. Evaluate In condition in filterbuilder class
        Null,
        NotNull,
        BeginsWith,
        DoesNotBeginWith,
        EndsWith,
        DoesNotEndsWith,
        Like,
        NotLike
    }
}
