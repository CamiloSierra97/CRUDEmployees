using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDEmployees.Models.WS
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string DocumentType { get; set; }
        public long DocumentNumber { get; set; }
        public DateTime DateOfHire { get; set; }
        public long? Phone { get; set; }
        public int SubAreaId { get; set; }
        public int CountryId { get; set; }
        public int? IsActive { get; set; }
    }
}