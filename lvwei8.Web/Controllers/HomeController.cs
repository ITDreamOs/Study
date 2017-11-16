using lvwei8.Redis;
using lvwei8.Service.Area;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lvwei8.Web.Controllers
{
    public class HomeController : Controller
    {

        public IAreaService AreaService { get; set; }

        public ActionResult Index()
        {
            var redistest = RedisService.GetService().Set<string>("teset", "lvwei8");
            var result = AreaService.GetAllProvinces();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}