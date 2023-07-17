using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DealerOnSalesTax.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace DealerOnSalesTax.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var model = initModel();

        CartItem ci = new CartItem();
        ci.Name = "Name1";
        ci.Category = "Food";
        ci.IsImported = true;
        ci.Price = 4.69;
        ci.Quantity = 2;
        model.CartItems.Add(ci);

        return View(model);
    }

    [HttpPost]
    public IActionResult GetReceipt([FromBody] IndexViewModel model)
    {
        var order = new OrderModel(model.CartItems);
        model.Receipt = order.GetReceipt();

        return Json(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private IndexViewModel initModel()
    {
        var model = new IndexViewModel();
        model.CartItems = new List<CartItem>();
        model.Categories = new SelectList(new[]
        {
            new SelectListItem { Value = "0", Text = "Uncategorized" },
            new SelectListItem { Value = "1", Text = "Book" },
            new SelectListItem { Value = "2", Text = "Food" },
            new SelectListItem { Value = "3", Text = "Medical" },
        }, "Value", "Text");
        return model;
    }
}

