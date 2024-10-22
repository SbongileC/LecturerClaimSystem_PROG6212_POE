using System.Reflection;
using LecturerClaimSystem.Models;
namespace LecturerClaimSystem.Models
{
    public class Claim
    {
        //For the Claim submission form
        public int Id { get; set; }
        public string LecturerName { get; set; }
        public string LecturerSurname { get; set; }
        public string EmployeeNumber { get; set; }
        public string ContactDetails { get; set; }
        public List<Module> Modules { get; set; }  
        public DateTime DateSubmitted { get; set; }
        public string SupportingDocumentPath { get; set; }

        // For status tracking
        public string Status { get; set; }  // Shows whether the claim is Pending, Verified or Approved
        public string VerifiedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string AdditionalNotes { get; set; }
        public string RejectionReason { get; set; }
    }
}


