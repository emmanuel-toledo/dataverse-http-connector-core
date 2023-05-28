namespace Dynamics.Crm.Http.Connector.Core.Utilities
{
    internal static class Check
    {
        public static string NotNullOrEmpty(string? value, string? parameterName)
        {
            if (string.IsNullOrEmpty(value))
            {
                NotNull(parameterName, nameof(parameterName));
                throw new ArgumentNullException(nameof(parameterName));
            }
            return value;
        }

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
