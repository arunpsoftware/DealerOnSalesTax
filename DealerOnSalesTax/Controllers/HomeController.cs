using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DealerOnSalesTax.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        var model = new IndexViewModel();
        model.OrderItems = new List<OrderItem>();

        model.Categories = new SelectList(new[]
        {
            new SelectListItem { Value = "0", Text = "Uncategorized" },
            new SelectListItem { Value = "1", Text = "Book" },
            new SelectListItem { Value = "2", Text = "Food" },
            new SelectListItem { Value = "3", Text = "Medical" },
        }, "Value", "Text");

        OrderItem oi = new OrderItem();
        oi.Name = "Name1";
        oi.Category = "Food";
        oi.IsImported = true;
        oi.Price = 4.69;
        oi.Quantity = 2;

        model.OrderItems.Add(oi);

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

