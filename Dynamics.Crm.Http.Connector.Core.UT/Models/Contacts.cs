using Dynamics.Crm.Http.Connector.Core.Domains.Annotations;

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

        /// <summary>
        /// Get and set record full name.
        /// </summary>
        [FieldAttributes("fullName", "fullname", FieldTypes.Text)]
        public string? FullName { get; set; }
    }
}
