using Dataverse.Http.Connector.Core.Domains.Annotations;

namespace Dataverse.Domain.Test.Models
{
    [Entity("contact", "contacts")]
    public class Contacts
    {
        [UniqueIdentifier("ContactId", "contactid")]
        public Guid Id { get; set; }

        [Text("FullName", "fullname", true)] // Read only column.
        public string? FullName { get; set; }

        [Text("FirstName", "firstname")]
        public string? FirstName { get; set; }

        [DateTimeOf("CreatedOn", "createdon")]
        public DateTime CreatedOn { get; set; }

        [DateTimeOf("ModifiedOn", "modifiedon")]
        public DateTime ModifiedOn { get; set; }

        [OptionSet("statuscode", "statuscode")]
        public int StatusCode { get; set; }

        [OptionSet("statecode", "statecode")]
        public int StateCode { get; set; }

        [Lookup("OwnerId", "ownerid", "systemusers")]
        public Guid OwnerId { get; set; }
    }
}
