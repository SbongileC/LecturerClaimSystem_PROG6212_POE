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
        public IActionResult SubmitClaim(Claim claim, IFormFile SupportingDocument)
        {
            if (SupportingDocument != null && SupportingDocument.Length > 0)
            {
                // Ensure the file extension is valid
                var validExtensions = new[] { ".pdf", ".jpg", ".png", ".docx" };
                var extension = Path.GetExtension(SupportingDocument.FileName).ToLowerInvariant();

                if (!validExtensions.Contains(extension))
                {
                    ModelState.AddModelError("SupportingDocument", "Invalid file format. Only PDF, JPG, PNG, and DOCX are allowed.");
                    return View(claim);
                }

                // Create the file name
                var fileName = $"{Guid.NewGuid()}{extension}";
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                // Ensure the directory exists
                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }

                var filePath = Path.Combine(uploadsPath, fileName);

                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    SupportingDocument.CopyTo(stream);
                }

                // Store the file path in the claim 
                claim.SupportingDocumentPath = $"/uploads/{fileName}";
            }

            // Auto-calculate the total payment
            if (claim.Modules != null && claim.Modules.Any())
            {
                claim.TotalPayment = claim.Modules.Sum(m => m.Payment);
            }
            else
            {
                ModelState.AddModelError("", "At least one module must be added.");
                return View(claim);
            }

            // Additional claim processing 
            claim.Status = "Pending";
            claim.DateSubmitted = DateTime.Now;
            claim.Id = claims.Count > 0 ? claims.Max(c => c.Id) + 1 : 1;

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
            claim.VerifiedBy = "Coordinator";
            return RedirectToAction("ClaimList");
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult RejectClaim(int id)
        {
         var claim = claims.FirstOrDefault(c => c.Id == id);

        if (claim == null || claim.Status == "Approved")
        {
        // Handle invalid claim or already approved claim
        return RedirectToAction("Error");
        }

        return View(claim); // Render the reject view with the claim details
        }

        [HttpPost]
        public IActionResult RejectClaim(int id, string rejectionReason)
        {
         var claim = claims.FirstOrDefault(c => c.Id == id);

        if (claim == null || claim.Status == "Approved")
        {
        // Handle invalid claim or already approved claim
        return RedirectToAction("Error");
        }

        // Reject the claim
        claim.Status = "Rejected";
        claim.RejectionReason = rejectionReason;

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
            claim.ApprovedBy = "Manager";  
            return RedirectToAction("ClaimList");
        }

        public IActionResult ClaimSubmitted()
        {
            return View();
        }
    }
}
