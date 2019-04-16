using Proyectotransmilenio.CargarArchivos;
using Proyectotransmilenio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyectotransmilenio.Controllers
{
    public class CargarDatosController : Controller
    {
        // GET: CargarDatos
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Laicon()
        {
            vistaEquiposLaicon vistaLaicon = new vistaEquiposLaicon();

            return View(vistaLaicon);
        }

        /// <summary>
        /// retorna vista de filtrado por proyecto 
        /// </summary>
        [HttpPost]
        public ActionResult Laicon(HttpPostedFileBase file)
        {
            string mensaje = "";
            if (Request.Files["file"].ContentLength > 0)
            {
                TempData.Remove("Mensaje");
                string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);

                string fileLocation = Server.MapPath("~/Content/") + Request.Files["file"].FileName;
                if (System.IO.File.Exists(fileLocation))
                {
                    System.IO.File.Delete(fileLocation);
                }
                Request.Files["file"].SaveAs(fileLocation);
                ReporteCargaArchivoModelo resultadoReporteLaicon = new ReporteCargaArchivoModelo();
                //----------Compara los nombres de las columnas del excel con el formato cargado en la herramienta
                ValidadorEstructuraLaicon leerLaicon = new ValidadorEstructuraLaicon();
                string resultadoEncabezados = leerLaicon.validarFormatoExcelLaicon(fileLocation);
                if (!string.IsNullOrWhiteSpace(resultadoEncabezados))
                {
                    resultadoReporteLaicon.Reporte = resultadoEncabezados;
                    System.IO.File.Delete(fileLocation);
                    mensaje = "El archivo " + Request.Files["file"].FileName + "se termino de procesar";
                    TempData["Mensaje"] = mensaje;
                    return View("Index", resultadoReporteLaicon);
                }
                System.IO.File.Delete(fileLocation);
                mensaje = "El archivo " + Request.Files["file"].FileName + " se ha cargado correctamente";
                TempData["Mensaje"] = mensaje;
                //variablesSession.guardarIdProyecto(IdProyecto);
                return RedirectToAction("Laicon", "Equipos");
            }
            else
            {
                mensaje = "No se ha seleccionado ningun archivo valido";
                TempData["Mensaje"] = mensaje;
                //variablesSession.guardarIdProyecto(IdProyecto);
                return RedirectToAction("Laicon", "Equipos");
            }
        }        
    }
}