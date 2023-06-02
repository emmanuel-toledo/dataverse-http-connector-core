using Dynamics.Crm.Http.Connector.Core.Domains.Annotations;

namespace Dynamics.Crm.Http.Connector.Core.UT.Models
{
    [EntityAttributes("crmit_identityuser", "crmit_identityusers")]
    internal class IdentityUser
    {
        [FieldAttributes("crmit_IdentityUserId", "crmit_identityuserid", FieldTypes.UniqueIdentifier)]
        public Guid IdentityUserId { get; set; }

        [FieldAttributes("crmit_Name", "crmit_name", FieldTypes.Text)]
        public string Name { get; set; }

        [FieldAttributes("crmit_Estatus", "crmit_estatus", FieldTypes.OptionSet)]
        public int Status { get; set; }

        [FieldAttributes("OwnerId", "ownerid", FieldTypes.Lookup)]
        public Guid OwnerId { get; set; }
    }
}
