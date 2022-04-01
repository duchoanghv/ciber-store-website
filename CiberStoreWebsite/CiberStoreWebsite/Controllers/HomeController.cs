using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CiberStoreWebsite.Models;
using CiberStoreWebsite.Bll;
using Microsoft.AspNetCore.Authorization;

namespace CiberStoreWebsite.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderBll _orderBll;

        public HomeController(ILogger<HomeController> logger, IOrderBll orderBll)
        {
            _logger = logger;
            _orderBll = orderBll;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Manage Orders";
            return View(await _orderBll.GetInitialData());
        }
        public async Task<PartialViewResult> DoSearchOrder(string categoryName)
        {
            return PartialView("_OrdersList_PartialView", await _orderBll.GetOrdersList(categoryName));
        }
        public async Task<ActionResult> DoSaveOrder(OrderModel order)
        {
            string msg = _orderBll.CheckValidityOfOrder(order);
            if (!string.IsNullOrEmpty(msg))
            {
                return Json(new { IsValid = false, Message = msg });
            }

            _orderBll.DoSaveOrder(order);
            return PartialView("_OrdersList_PartialView", await _orderBll.GetOrdersList());
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
