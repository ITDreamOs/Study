using lvwei8.Service.Area;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lvwei8.WebAPI.Controllers
{
    public class HomeController : Controller
    {

        #region 服务
    
        #endregion
        public ActionResult Index()
        {
         
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
