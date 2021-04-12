using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecSample.Models;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Xml;

namespace SecSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string name)
        {
            try
            {
                using var conn = new SqlConnection("Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;");

                // Sql Injection http://projects.webappsec.org/w/page/13246963/SQL%20Injection

                var cmd = new SqlCommand($"Select * from mytable where name='{name}'", conn);
                cmd.ExecuteNonQuery();


            }
            catch
            {

            }

            return View();
        }

        public string Privacy(string myParam)
        {
            try
            {
                var p = new Process();
                p.StartInfo.FileName = "exportLegacy.exe";
                p.StartInfo.Arguments = " -user " + myParam + " -role user";
                p.Start();

                var doc = new XmlDocument { XmlResolver = null };
                doc.Load("/config.xml");
                var results = doc.SelectNodes("/Config/Devices/Device[id='" + myParam + "']");


            }
            catch { }

            // not XSS
            return "Value " + myParam;
        }

        [HttpPost]
        public ActionResult ControllerMethod(string input)
        {
            return null;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
