using Microsoft.AspNetCore.Mvc;

namespace HMK_PROJECT.Controllers
{
    public class WelcomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string HelloWorld()
        {
            return "Hello World action: Hello world";
        }
    }
}