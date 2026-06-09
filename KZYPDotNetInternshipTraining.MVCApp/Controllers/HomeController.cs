using KZYPDotNetInternshipTraining.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KZYPDotNetInternshipTraining.MVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index([FromQuery] HomeRequestModel request)
        {
            var responseModel = new HomeResponseModel
            {
                Id = request.Id,
                PageNo = request.PageNo,
                PageSize = request.PageSize
            };
            return View(responseModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
