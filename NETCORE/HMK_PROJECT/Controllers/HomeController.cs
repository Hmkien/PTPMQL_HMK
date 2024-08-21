using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HMK_PROJECT.Models;

namespace HMK_PROJECT.Controllers;

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
    [HttpPost]
    public IActionResult Index(Person ps)
    {

        string str = "Xin chào" + ps.PersonId + "có tên là " + ps.FullName + " Sống ở " + ps.Address;
        ViewData["Input"] = str;
        return View();

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
