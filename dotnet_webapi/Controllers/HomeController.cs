using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Emit;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Serilog.Context;
using Serilog;

namespace dotnet_webapi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            this._logger = logger;
        }

        public IActionResult Index()
        {
            using (LogContext.PushProperty("type1", true))
            {
                // Process request; all logged events will carry `event_type`
                Log.Information("1.This log message will have an event_type property");
                Log.Warning("2.ABCD");
                Log.Error("3.ABCD");
                Log.Fatal("4.ABCD");
                Log.Fatal("-------------------------------------------------------");
            }
            return Ok("Hello world -> 1");
        }

        public IActionResult Index2()
        {
            using (LogContext.PushProperty("type2", true))
            {
                // Process request; all logged events will carry `event_type`
                Log.Information("1.type2");
                Log.Warning("2.ABCD");
                Log.Error("3.ABCD");
                Log.Fatal("4.ABCD");
                Log.Fatal("-------------------------------------------------------");
            }
            return Ok("Hello world -> 2");
        }


    }
}