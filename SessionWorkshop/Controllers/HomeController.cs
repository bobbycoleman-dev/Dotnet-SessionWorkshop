using System.Diagnostics;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using SessionWorkshop.Models;

namespace SessionWorkshop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("login")]
    public IActionResult Login(string name)
    {
        HttpContext.Session.SetString("User", name);
        HttpContext.Session.SetInt32("Number", 22);
        return RedirectToAction("Dashboard");
    }

    [HttpGet("dashboard")]
    public IActionResult Dashboard()
    {
        
        if(HttpContext.Session.GetString("User") == null){
            return RedirectToAction("Index");
        }
        
        return View();
    }

    [HttpPost("math")]
    public IActionResult Math(string math)
    {
        int? Number = HttpContext.Session.GetInt32("Number");
        int NewNumber = (int)Number;
        switch(math){
            case "add":
                NewNumber += 1;
                HttpContext.Session.SetInt32("Number", NewNumber);
                break;
            case "subtract":
                NewNumber -= 1;
                HttpContext.Session.SetInt32("Number", NewNumber);
                break;
            case "multiply":
                NewNumber *= 2;
                HttpContext.Session.SetInt32("Number", NewNumber);
                break;
            case "random":
                Random rand = new();
                int RandomNumber = rand.Next(1,11);
                NewNumber += RandomNumber;
                HttpContext.Session.SetInt32("Number", NewNumber);
                break;
            default:
                break;
        }
        return RedirectToAction("Dashboard");
    }

    [HttpPost("logout")]
    public RedirectToActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
