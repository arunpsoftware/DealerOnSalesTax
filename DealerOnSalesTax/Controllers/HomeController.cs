﻿using System.Diagnostics;
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
        var model = initModel();

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    /// <summary>
    /// Receive a list of cart items (bound to viewmodel).
    /// </summary>
    /// <param name="model">IndexViewModel</param>
    /// <returns>JSON object with receipt.</returns>
    [HttpPost]
    public IActionResult GetReceipt([FromBody] IndexViewModel model)
    {
        var order = new Order(model.CartItems);
        model.Receipt = order.GetReceipt();

        return Json(model);
    }

    /// <summary>
    /// Create our view model and populate the list of item categories.
    /// </summary>
    /// <returns>The viewmodel to display on the homepage.</returns>
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

