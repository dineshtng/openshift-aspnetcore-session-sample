using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreSessionSample.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IDataProtectionProvider p)
        {
                Console.WriteLine(p.GetType());
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            int visitCount = 1;
            byte[] bytes;
            if (HttpContext.Session.TryGetValue("VisitCount", out bytes))
            {
                visitCount = Convert.ToInt32(bytes[0]) + 1;
            }
            HttpContext.Session.Set("VisitCount", new byte[]{Convert.ToByte(visitCount)});
            ViewBag.VisitCount = visitCount;

            var hostName = System.IO.File.ReadAllText("/etc/hostname");
            ViewBag.HostName = hostName;
            ViewBag.SessionId = HttpContext.Session.Id;
            ViewBag.Cookieid = HttpContext.Request.Cookies[".AspNetCore.Session"];
            return View();
        }
    }
}
