namespace Dataverse.Http.Connector.Core.Utilities
{
    /// <summary>
    /// Utility class to parse the type of a value.
    /// </summary>
    internal static class Parse
    {
        /// <summary>
        /// Function to change the type of a value.
        /// </summary>
        /// <typeparam name="T">Convertion value type.</typeparam>
        /// <typeparam name="V">Current value type.</typeparam>
        /// <param name="value">Current value.</param>
        /// <param name="code">Value type code.</param>
        /// <returns>Specified convertion value type.</returns>
        private static T? ChangeType<T, V>(V value, TypeCode code)
            => (T?)Convert.ChangeType(value, code);

        /// <summary>
        /// Function to parse a value in a string to use it to create a condition tag in FetchXml query.
        /// </summary>
        /// <typeparam name="T">Property type of a class.</typeparam>
        /// <param name="value">Value of property.</param>
        /// <returns>Parsed value to string.</returns>
        /// <exception cref="ArgumentNullException">There is not a valid value.</exception>
        public static string ParseValue<T>(T value)
        {
            string parsedValue = string.Empty;
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.String:
                    parsedValue = ChangeType<string, T>(value, TypeCode.String) ?? "";
                    break;
                case TypeCode.Int16:
                    parsedValue = ChangeType<short, T>(value, TypeCode.String).ToString() ?? "";
                    break;
                case TypeCode.Int32:
                    parsedValue = ChangeType<int, T>(value, TypeCode.String).ToString() ?? "";
                    break;
                case TypeCode.Int64:
                    parsedValue = ChangeType<long, T>(value, TypeCode.String).ToString() ?? "";
                    break;
                case TypeCode.Double:
                    parsedValue = ChangeType<double, T>(value, TypeCode.String).ToString() ?? "";
                    break;
                case TypeCode.Single:
                    parsedValue = ChangeType<float, T>(value, TypeCode.String).ToString() ?? "";
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
                        parsedValue = value!.ToString() ?? Guid.Empty.ToString();
                    else
                        parsedValue = ChangeType<string, T>(value, TypeCode.String) ?? "";
                    break;
                default:
                    throw new ArgumentNullException(nameof(value));
            }
            return parsedValue;
        }
    }
}
