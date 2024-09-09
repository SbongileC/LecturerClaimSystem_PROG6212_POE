using System.Reflection;
using LecturerClaimSystem.Models;
namespace LecturerClaimSystem.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public string LecturerName { get; set; }
        public string LecturerSurname { get; set; }
        public string EmployeeNumber { get; set; }
        public string ContactDetails { get; set; }
        public List<Module> Modules { get; set; }  
        public DateTime DateSubmitted { get; set; }
        public string SupportingDocumentPath { get; set; }

        // For status tracking
        public string Status { get; set; }  // Pending, Verified, Approved
        public string VerifiedBy { get; set; }
        public string ApprovedBy { get; set; }
    }
}

namespace LecturerClaimSystem.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public int Credits { get; set; }
    }
}
