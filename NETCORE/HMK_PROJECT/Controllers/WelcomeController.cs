using Microsoft.AspNetCore.Mvc;

namespace HMK_PROJECT.Controllers
{
    public class WelcomeController : Controller
    {
        public string Index()
        {
            return "Welcome to PTPMQL đây là action Index";
        }

        public string HelloWorld()
        {
            return "Hello World action: Hello world";
        }
    }
}