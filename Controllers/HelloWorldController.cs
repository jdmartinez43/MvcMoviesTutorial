using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers;

/* The HelloWorldController organizes endpoints used for the HelloWorld endpoints
   meaning that for the URLS that start like: [webaddress]/HelloWorld/[x] 
   use this specific controller. The [x] represents the specific action
   called for the controller.*/
public class HelloWorldController : Controller
{
    // 
    // GET: /HelloWorld/
    public IActionResult Index()
{
    return View();
}
    // 
    // GET: /HelloWorld/Welcome/ 
    // Requires using System.Text.Encodings.Web;
    public IActionResult Welcome(string name, int ID = 1, int numTimes = 1)
{
    ViewData["Message"] = "Hello " + name;
    ViewData["ID"] = ID;
    ViewData["NumTimes"] = numTimes;
    return View();
}
}