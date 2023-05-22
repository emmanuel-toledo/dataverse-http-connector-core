using Dynamics.Crm.Http.Connector.Core.Models.Annotations;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Annotations;

namespace Dynamics.Crm.Http.Connector.Core.UT.Models
{
    /// <summary>
    /// Contact entity class deffinition.
    /// </summary>
    [EntityAttributes("contact", "contacts")]
    public class Contacts
    {
        /// <summary>
        /// Get and set Entity unique identifier.
        /// </summary>
        [FieldAttributes("contactId", "contactid", FieldTypes.UniqueIdentifier)]
        public Guid Id { get; set; }
    }
}
