using Dataverse.Http.Connector.Core.Domains.Annotations;

namespace Dataverse.Domain.Test.Models
{
    [Entity("contact", "contacts")]
    public class Contacts
    {
        [Column("ContactId", "contactid", ColumnTypes.UniqueIdentifier)]
        public Guid Id { get; set; }

        [Column("FirstName", "firstname", ColumnTypes.Text)]
        public string? FirstName { get; set; }

        [Column("CreatedOn", "createdon", ColumnTypes.DateTime)]
        public DateTime CreatedOn { get; set; }

        [Column("ModifiedOn", "modifiedon", ColumnTypes.DateTime)]
        public DateTime ModifiedOn { get; set; }

        [Column("statuscode", "statuscode", ColumnTypes.OptionSet)]
        public int StatusCode { get; set; }

        [Column("statecode", "statecode", ColumnTypes.OptionSet)]
        public int StateCode { get; set; }

        [Column("OwnerId", "ownerid", ColumnTypes.Lookup, "systemusers")]
        public Guid OwnerId { get; set; }
    }
}
