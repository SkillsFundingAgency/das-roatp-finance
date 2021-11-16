using SFA.DAS.RoatpFinance.Web.Settings;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.RoatpFinance.Web.ViewModels.Errors;

namespace SFA.DAS.RoatpFinance.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebConfiguration _configuration;

        public HomeController(IWebConfiguration configuration)
        {
            _configuration = configuration;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            return RedirectToAction("OpenApplications", "RoatpFinancial");
        }

        [Route("/Dashboard")]
        public IActionResult Dashboard()
        {
            return Redirect(_configuration.EsfaAdminServicesBaseUrl + "/Dashboard");
        }
    }
}
