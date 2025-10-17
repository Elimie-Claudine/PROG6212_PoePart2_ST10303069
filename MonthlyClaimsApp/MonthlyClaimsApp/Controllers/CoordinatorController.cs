using Microsoft.AspNetCore.Mvc;
using MonthlyClaimsApp.Models;

namespace MonthlyClaimsApp.Controllers
{
    public class CoordinatorController : Controller
    {
        private static List<Claim> _claims => LecturerControllerHelper.GetClaims();

        public IActionResult Index()
        {
            return View(_claims);
        }

        [HttpPost]
        public IActionResult VerifyClaim(int id, string actionType)
        {
            var claim = _claims.FirstOrDefault(c => c.ClaimID == id);
            if (claim != null)
            {
                claim.Status = actionType == "approve" ? "Approved by Coordinator" : "Rejected by Coordinator";
                TempData["Message"] = $"Claim ID {claim.ClaimID} {claim.Status}.";
            }
            return RedirectToAction("Index");
        }
    }

    public static class LecturerControllerHelper
    {
        private static List<Claim> _sharedClaims = new List<Claim>();
        public static List<Claim> GetClaims() => _sharedClaims;
    }
}
