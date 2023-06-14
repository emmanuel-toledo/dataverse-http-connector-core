using Dataverse.Http.Connector.Core.Domains.Annotations;

namespace Dataverse.Domain.Test.Models
{
    [Entity("crmit_employee", "crmit_employees")]
    public class Employees
    {
        [Field("crmit_EmployeeId", "crmit_employeeid", FieldTypes.UniqueIdentifier)]
        public Guid Id { get; set; }

        [Field("crmit_Name", "crmit_name", FieldTypes.Text)]
        public string? Name { get; set; }

        [Field("crmit_EmployeeNumber", "crmit_employeenumber", FieldTypes.Text)]
        public string? EmployeeNumber { get; set; }

        [Field("CreatedOn", "createdon", FieldTypes.DateTime)]
        public DateTime CreatedOn { get; set; }

        [Field("ModifiedOn", "modifiedon", FieldTypes.DateTime)]
        public DateTime ModifiedOn { get; set; }

        [Field("statuscode", "statuscode", FieldTypes.OptionSet)]
        public int StatusCode { get; set; }

        [Field("statecode", "statecode", FieldTypes.OptionSet)]
        public int StateCode { get; set; }

        [Field("crmit_IsDeleted", "crmit_isdeleted", FieldTypes.BoolOptionSet)]
        public bool IsDeleted { get; set; }

        [Field("OwnerId", "ownerid", FieldTypes.Lookup, "systemusers")]
        public Guid OwnerId { get; set; }
    }
}
