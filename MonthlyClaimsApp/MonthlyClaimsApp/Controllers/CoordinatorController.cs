using Microsoft.AspNetCore.Mvc;
using MonthlyClaimsApp.Models;

namespace MonthlyClaimsApp.Controllers
{
    public class CoordinatorController : Controller
    {
        private static List<Claim> _claims => LecturerControllerHelper.GetClaims();

        public IActionResult SelectRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetRole(string role)
        {
            HttpContext.Session.SetString("UserRole", role);
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (string.IsNullOrEmpty(userRole))
            {
                return RedirectToAction("SelectRole");
            }

            ViewBag.UserRole = userRole;
            return View(_claims);
        }

        [HttpPost]
        public IActionResult VerifyClaim(int id, string actionType)
        {
            var claim = _claims.FirstOrDefault(c => c.ClaimID == id);
            if (claim != null)
            {
                var userRole = HttpContext.Session.GetString("UserRole") ?? "Coordinator";

                if (actionType == "approve")
                {
                    claim.Status = $"Approved by {userRole}";
                    TempData["Message"] = $"Claim ID {claim.ClaimID} approved by {userRole}.";
                }
                else
                {
                    claim.Status = $"Rejected by {userRole}";
                    TempData["ErrorMessage"] = $"Claim ID {claim.ClaimID} rejected by {userRole}.";
                }

                claim.VerifiedByRole = userRole;
                claim.VerifiedBy = User?.Identity?.Name ?? userRole;
                claim.VerifiedDate = DateTime.Now;
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
