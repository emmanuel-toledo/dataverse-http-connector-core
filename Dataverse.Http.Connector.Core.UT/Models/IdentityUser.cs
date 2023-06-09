using Dataverse.Http.Connector.Core.Domains.Annotations;

namespace Dataverse.Http.Connector.Core.UT.Models
{
    [EntityAttributes("crmit_identityuser", "crmit_identityusers")]
    internal class IdentityUser
    {
        [FieldAttributes("crmit_IdentityUserId", "crmit_identityuserid", FieldTypes.UniqueIdentifier)]
        public Guid IdentityUserId { get; set; }

        [FieldAttributes("crmit_Name", "crmit_name", FieldTypes.Text)]
        public string Name { get; set; }

        [FieldAttributes("crmit_Edad", "crmit_edad", FieldTypes.Number)]
        public int Age { get; set; }

        [FieldAttributes("crmit_Estatus", "crmit_estatus", FieldTypes.OptionSet)]
        public int Status { get; set; }

        [FieldAttributes("statecode", "statecode", FieldTypes.OptionSet)]
        public int StateCode { get; set; }

        [FieldAttributes("CreatedOn", "createdon", FieldTypes.DateTime)]
        public DateTime CreatedOn { get; set; }

        [FieldAttributes("OwnerId", "ownerid", FieldTypes.Lookup, "systemusers")]
        public Guid OwnerId { get; set; }
    }
}
