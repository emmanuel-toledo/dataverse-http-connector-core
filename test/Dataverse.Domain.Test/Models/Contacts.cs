using Dataverse.Http.Connector.Core.Domains.Annotations;

namespace Dataverse.Domain.Test.Models
{
    [Entity("contact", "contacts")]
    public class Contacts
    {
        [Field("ContactId", "contactid", FieldTypes.UniqueIdentifier)]
        public Guid Id { get; set; }

        [Field("FirstName", "firstname", FieldTypes.Text)]
        public string? FirstName { get; set; }

        [Field("CreatedOn", "createdon", FieldTypes.DateTime)]
        public DateTime CreatedOn { get; set; }

        [Field("ModifiedOn", "modifiedon", FieldTypes.DateTime)]
        public DateTime ModifiedOn { get; set; }

        [Field("statuscode", "statuscode", FieldTypes.OptionSet)]
        public int StatusCode { get; set; }

        [Field("statecode", "statecode", FieldTypes.OptionSet)]
        public int StateCode { get; set; }

        [Field("OwnerId", "ownerid", FieldTypes.Lookup, "systemusers")]
        public Guid OwnerId { get; set; }
    }
}
