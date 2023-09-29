#pragma warning disable CS8629
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SessionWorkshop.Models;

namespace SessionWorkshop.Controllers;

public class HomeController : Controller
{
    //* Index View
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    //* Login/Set Session Action
    [HttpPost("login")]
    public IActionResult Login(User newUser)
    {
        //! Form Error Validations
        if(!ModelState.IsValid)
        {
            return View("Index");
        }

        //! Set Session User and Number; Redirect to Dashboard
        HttpContext.Session.SetString("User", newUser.Name);
        HttpContext.Session.SetInt32("Number", 22);
        return RedirectToAction("Dashboard");
    }

    //* Dashboard View
    [HttpGet("dashboard")]
    public IActionResult Dashboard()
    {
        //! Check if user signed in
        if(HttpContext.Session.GetString("User") == null){
            return RedirectToAction("Index");
        }
        
        return View();
    }

    //* Update Session Int
    [HttpPost("math")]
    public IActionResult Math(string math)
    {
        //! Get current Session Int and cast as int
        int? Number = HttpContext.Session.GetInt32("Number");
        int NewNumber = (int)Number;

        //! Perform Math Function
        switch(math){
            case "add":
                NewNumber += 1;
                break;
            case "subtract":
                NewNumber -= 1;
                break;
            case "multiply":
                NewNumber *= 2;
                break;
            case "random":
                Random rand = new();
                int RandomNumber = rand.Next(1,11);
                NewNumber += RandomNumber;
                break;
            default:
                break;
        }

        //! Set new Session Int & Redirect to Dashboard
        HttpContext.Session.SetInt32("Number", NewNumber);
        return RedirectToAction("Dashboard");
    }


    //* Logout/Clear Session Action
    [HttpPost("logout")]
    public RedirectToActionResult Logout()
    {
        //! Clear All Session
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
