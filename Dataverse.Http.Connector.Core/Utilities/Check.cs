namespace Dataverse.Http.Connector.Core.Utilities
{
    /// <summary>
    /// Utility class to validate values.
    /// </summary>
    internal static class Check
    {
        /// <summary>
        /// Function to validate if an string value is null or empty according to a parameter.
        /// </summary>
        /// <param name="value">Value content.</param>
        /// <param name="parameterName">Value parameter name.</param>
        /// <returns>String value.</returns>
        /// <exception cref="ArgumentNullException">The value is null.</exception>
        public static string NotNullOrEmpty(string? value, string? parameterName)
        {
            if (string.IsNullOrEmpty(value))
            {
                NotNull(parameterName, nameof(parameterName));
                throw new ArgumentNullException(nameof(parameterName));
            }
            return value;
        }

        /// <summary>
        /// Function to validate if an value is null.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="value">Value content.</param>
        /// <param name="parameterName">Value parameter name.</param>
        /// <returns>Value of type T.</returns>
        /// <exception cref="ArgumentNullException">The value is null.</exception>
        public static T NotNull<T>(T? value, string? parameterName)
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
