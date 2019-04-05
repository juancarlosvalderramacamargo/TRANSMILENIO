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
            ViewBag.Message = "Colaboradores";

            return View();
        }

        public ActionResult ModeloER()
        {
            var path = Server.MapPath("Content/Images/") + "modelER.jpeg";
            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            string imageBase64Data = Convert.ToBase64String(imageByteData);
            string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
            return new FileContentResult(imageByteData, "image/jpeg");
        }
    }
}