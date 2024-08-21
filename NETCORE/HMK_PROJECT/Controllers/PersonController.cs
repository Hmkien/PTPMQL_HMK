using Microsoft.AspNetCore.Mvc;

namespace HMK_PROJECT.Controllers
{
    public class PersonController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}