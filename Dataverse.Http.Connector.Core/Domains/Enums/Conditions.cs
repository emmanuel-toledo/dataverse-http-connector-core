namespace Dataverse.Http.Connector.Core.Domains.Enums
{
    /// <summary>
    /// This enum works to define the types of conditions that we can use in a FetchXml query.
    /// </summary>
    public enum ConditionTypes
    {
        // Multi property type that require one value.
        Equal,
        NotEqual,
        // Multi property type that don't require value.
        Null,
        NotNull,
        // Multi property type that require one value.
        BeginsWith,
        DoesNotBeginWith,
        EndsWith,
        DoesNotEndsWith,
        Like,
        NotLike,
        // Multi property type that require more that one value.
        In,
        NotIn,
        Between,
        NotBetween,
        // Multi property that require value.
        GreaterThan,
        GreaterEqual,
        LessThan,
        LessEqual,
        // DateTimes that don't require value.
        // Last date time.
        Last7Days,
        LastFiscalPeriod,
        LastFiscalYear,
        LastMonth,
        LastWeek,
        LastYear,
        // Next date time.
        Next7Days,
        NextFiscalPeriod,
        NextFiscalYear,
        NextMonth,
        NextWeek,
        NextYear,
        // Current date time.
        ThisFiscalPeriod,
        ThisFiscalYear,
        ThisMonth,
        ThisWeek,
        ThisYear,
        Today,
        Tomorrow,
        Yesterday
    }

    /// <summary>
    /// This class defines the condition types that can be used in a FetchXml query.
    /// </summary>
    internal static class Conditions
    {
        /// <summary>
        /// Function to parse a Condition Type in a FetchXml condition string.
        /// </summary>
        /// <param name="conditionType">Enum condition type</param>
        /// <returns>FetchXml condition string.</returns>
        /// <exception cref="ArgumentNullException">Condition type was not recognized.</exception>
        internal static string Parse(ConditionTypes conditionType)
        {
            return conditionType switch
            {
                // Multi property type that require one value.
                ConditionTypes.Equal => "eq",
                ConditionTypes.NotEqual => "ne",
                // Multi property type that don't require value.
                ConditionTypes.Null => "null",
                ConditionTypes.NotNull => "not-null",
                // Multi property type that require one value.
                ConditionTypes.BeginsWith => "begins-with",
                ConditionTypes.DoesNotBeginWith => "not-begin-with",
                ConditionTypes.EndsWith => "ends-with",
                ConditionTypes.DoesNotEndsWith => "not-end-with",
                ConditionTypes.Like => "like",
                ConditionTypes.NotLike => "not-like",
                // Multi property type that require more that one value.
                ConditionTypes.In => "in",
                ConditionTypes.NotIn => "not-in",
                ConditionTypes.Between => "between",
                ConditionTypes.NotBetween => "not-between",
                // Multi property that require value.
                ConditionTypes.GreaterThan => "gt",
                ConditionTypes.GreaterEqual => "ge",
                ConditionTypes.LessThan => "lt",
                ConditionTypes.LessEqual => "le",
                // DateTimes that don't require value (last date time).
                ConditionTypes.Last7Days => "last-seven-days",
                ConditionTypes.LastFiscalPeriod => "last-fiscal-period",
                ConditionTypes.LastFiscalYear => "last-fiscal-year",
                ConditionTypes.LastMonth => "last-month",
                ConditionTypes.LastWeek => "last-week",
                ConditionTypes.LastYear => "last-year",
                // DateTimes that don't require value (next date time).
                ConditionTypes.Next7Days => "next-seven-days",
                ConditionTypes.NextFiscalPeriod => "next-fiscal-period",
                ConditionTypes.NextFiscalYear => "next-fiscal-year",
                ConditionTypes.NextMonth => "next-month",
                ConditionTypes.NextWeek => "next-week",
                ConditionTypes.NextYear => "next-year",
                // DateTimes that don't require value (current date time).
                ConditionTypes.ThisFiscalPeriod => "this-fiscal-period",
                ConditionTypes.ThisFiscalYear => "this-fiscal-year",
                ConditionTypes.ThisMonth => "this-month",
                ConditionTypes.ThisWeek => "this-week",
                ConditionTypes.ThisYear => "this-year",
                ConditionTypes.Today => "today",
                ConditionTypes.Tomorrow => "tomorrow",
                ConditionTypes.Yesterday => "yesterday",
                _ => throw new NullReferenceException("Any condition type was not selected."),
            };
        }
    }
}
