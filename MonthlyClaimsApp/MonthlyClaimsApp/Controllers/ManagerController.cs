using Microsoft.AspNetCore.Mvc;
using MonthlyClaimsApp.Models;

namespace MonthlyClaimsApp.Controllers
{
    public class ManagerController : Controller
    {
        private static List<Claim> _claims = LecturerControllerHelper.GetClaims();

        public IActionResult Index()
        {
            return View(_claims);
        }

        [HttpPost]
        public IActionResult ApproveClaim(int id)
        {
            var claim = _claims.FirstOrDefault(c => c.ClaimID == id);
            if (claim != null)
            {
                claim.Status = "Approved by Manager";
            }
            return View("Index", _claims);
        }

        [HttpPost]
        public IActionResult RejectClaim(int id)
        {
            var claim = _claims.FirstOrDefault(c => c.ClaimID == id);
            if (claim != null)
            {
                claim.Status = "Rejected by Manager";
            }
            return View("Index", _claims);
        }
    }
}
