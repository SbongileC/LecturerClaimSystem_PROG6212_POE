using LecturerClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LecturerClaimSystem.Controllers
{
    public class HomeController : Controller
    {
        // In-memory list to hold claims
        private static List<Claim> claims = new List<Claim>();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ClaimList()
        {
            // Return the list of claims
            return View(claims);
        }

        public IActionResult SubmitClaim()
        {
            // Ensure this action returns the correct view
            return View();
        }

        [HttpPost]
        public IActionResult SubmitClaim(Claim claim)
        {
            // Ensure you're handling the claim submission correctly and returning a view or redirecting
            claim.Status = "Pending";
            claim.DateSubmitted = DateTime.Now;

            // Add the claim to the in-memory list (or another storage solution)
            claims.Add(claim);

            return RedirectToAction("ClaimSubmitted");
        }




        public IActionResult VerifyClaim(int id)
        {
            var claim = claims.FirstOrDefault(c => c.Id == id);

            if (claim == null || claim.Status != "Pending")
            {
                // Handle invalid claim or already verified
                return RedirectToAction("Error");
            }

            // Verify the claim
            claim.Status = "Verified";
            claim.VerifiedBy = "Coordinator";  // For now, use a placeholder name
            return RedirectToAction("ClaimList");
        }

        public IActionResult ApproveClaim(int id)
        {
            var claim = claims.FirstOrDefault(c => c.Id == id);

            if (claim == null || claim.Status != "Verified")
            {
                // Handle invalid claim or not yet verified
                return RedirectToAction("Error");
            }

            // Approve the claim
            claim.Status = "Approved";
            claim.ApprovedBy = "Manager";  // For now, use a placeholder name
            return RedirectToAction("ClaimList");
        }

        public IActionResult ClaimSubmitted()
        {
            return View();
        }
    }
}
