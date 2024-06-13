using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace dotnet_webapi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Hello world");
        }


    }
}