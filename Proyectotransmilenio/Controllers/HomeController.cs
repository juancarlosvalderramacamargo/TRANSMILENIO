using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyectotransmilenio.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "TRANSMILENIO";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Desarrollado por";

            return View();
        }
    }
}