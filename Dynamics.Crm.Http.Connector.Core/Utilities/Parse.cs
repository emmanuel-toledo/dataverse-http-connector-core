using Dynamics.Crm.Http.Connector.Core.Domains.Enums;

namespace Dynamics.Crm.Http.Connector.Core.Utilities
{
    internal static class Parse
    {
        private static T? ChangeType<T, V>(V value, TypeCode code)
            => (T?)Convert.ChangeType(value, code);

        public static string ParseValue<T>(T value)
        {
            string? parsedValue = string.Empty;
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.String:
                    parsedValue = ChangeType<string, T>(value, TypeCode.String) ?? "";
                    break;
                case TypeCode.Int32:
                    parsedValue = ChangeType<int, T>(value, TypeCode.String).ToString() ?? "";
                    break;
                case TypeCode.Double:
                    parsedValue = ChangeType<double, T>(value, TypeCode.String).ToString() ?? "";
                    break;
                case TypeCode.Decimal:
                    parsedValue = ChangeType<decimal, T>(value, TypeCode.String).ToString() ?? "";
                    break;
                case TypeCode.Boolean:
                    parsedValue = ChangeType<bool, T>(value, TypeCode.String).ToString() ?? "";
                    break;
                case TypeCode.DateTime:
                    parsedValue = ChangeType<DateTime, T>(value, TypeCode.String).ToString("yyyy-MM-ddTHH:mm:ssZ") ?? "";
                    break;
                case TypeCode.Object:
                    if (typeof(T) == typeof(Guid))
                        parsedValue = ChangeType<Guid, T>(value, TypeCode.String).ToString() ?? "";
                    else
                        throw new ArgumentNullException(nameof(value));
                    break;
                default:
                    throw new ArgumentNullException(nameof(value));
            }
            return parsedValue;
        }

        public static string ParseCondition(ConditionTypes conditionType)
        {
            string conditionString = string.Empty;
            switch (conditionType)
            {
                case ConditionTypes.Equal:
                    conditionString = "eq";
                    break;
                case ConditionTypes.NotEqual:
                    conditionString = "ne";
                    break;
                case ConditionTypes.Null:
                    conditionString = "null";
                    break;
                case ConditionTypes.NotNull:
                    conditionString = "not-null";
                    break;
                case ConditionTypes.BeginsWith:
                    conditionString = "begins-with";
                    break;
                case ConditionTypes.DoesNotBeginWith:
                    conditionString = "not-begin-with";
                    break;
                case ConditionTypes.EndsWith:
                    conditionString = "ends-with";
                    break;
                case ConditionTypes.DoesNotEndsWith:
                    conditionString = "not-end-with";
                    break;
                case ConditionTypes.Like:
                    conditionString = "like";
                    break;
                case ConditionTypes.NotLike:
                    conditionString = "not-like";
                    break;
                default:
                    throw new ArgumentNullException(nameof(conditionType));
            }
            return conditionString;
        }
    }
}
