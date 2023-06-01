using Dynamics.Crm.Http.Connector.Core.Domains.Enums;

namespace Dynamics.Crm.Http.Connector.Core.Domains.Dynamics.Context
{
    public class Request
    {
        public Request() {}

        public Request(HttpMethod methodType, string endpoint)
        {
            MethodType = methodType;
            EndPoint = endpoint;
        }

        public HttpMethod MethodType { get; set; } = HttpMethod.Get;

        public string? EndPoint { get; set; } = string.Empty;

        public BaseMethod Method { get; set; } = BaseMethod.None;

		public Dictionary<string, string> Params { get; set; } = new();

        public void AddParam(string key, string value)
            => Params.Add(key, value);
    }
}
