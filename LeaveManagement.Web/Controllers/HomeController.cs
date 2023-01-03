using LeaveManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LeaveManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

      
        public IActionResult Privacy()
        {
            return View();
        }
        /// <summary>
        /// this method will be called only in a unhadnled exception (Every time we code "throw" which means collapse the entire program
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if(exceptionHandlerPathFeature != null)
            {
                Exception exception = exceptionHandlerPathFeature.Error;//pull the error message out
                _logger.LogError(exception, $"Error Encountered by User:{this.User?.Identity.Name} | Request Id:{requestId}");
            }
            return View(new ErrorViewModel { RequestId = requestId });
        }
    }
}