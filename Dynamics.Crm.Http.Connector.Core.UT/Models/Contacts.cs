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

		/// <summary>
		/// Get and set created on.
		/// </summary>
		[FieldAttributes("createdOn", "createdon", FieldTypes.DateTime)]
		public DateTime CreatedOn { get; set; }

		/// <summary>
		/// Get and set owner unique identifier.
		/// </summary>
		[FieldAttributes("ownerId", "ownerid", FieldTypes.Lookup)]
        public Guid OwnerId { get; set; }
	}
}
