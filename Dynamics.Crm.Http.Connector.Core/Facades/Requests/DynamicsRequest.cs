namespace  Dynamics.Crm.Http.Connector.Core.Facades.Requests
{
    /// <summary>
    /// This interface defines the method to call each request to Dynamics.
    /// </summary>
    public interface IDynamicsRequest
    {
        /// <summary>
        /// Method to send a request to SharePoint
        /// </summary>
        /// <param name="request">Http request message configuration.</param>
        /// <returns>Http response message.</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }

    /// <summary>
    /// This class implements the method to call each request to Dynamics.
    /// </summary>
    public class DynamicsRequest : IDynamicsRequest
    {
        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            throw new NotImplementedException();
        }
    }
}
