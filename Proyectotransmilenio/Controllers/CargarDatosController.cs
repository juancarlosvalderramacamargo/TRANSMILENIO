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
            IdProyecto = variablesSession.obtenerIdProyecto();
            List<TGProyectos_EquiposLaicon> equiposLaiconTesgestion = new List<TGProyectos_EquiposLaicon>();
            List<EquiposLaicon> equiposLaiconVista = new List<EquiposLaicon>();
            vistaEquiposLaicon vistaLaicon = new vistaEquiposLaicon();
            vistaLaicon.listaTiposEquipo = ObtenerListaTiposLaicon("");
            string tipo = vistaLaicon.listaTiposEquipo.FirstOrDefault().Text;
            using (TesGestionProyectosEntities dbContex = new TesGestionProyectosEntities())
            {
                equiposLaiconTesgestion = dbContex.TGProyectos_EquiposLaicon.Where(x => x.ElementType == tipo).ToList();
            }
            foreach (TGProyectos_EquiposLaicon equipoLaiconTesgestion in equiposLaiconTesgestion)
            {
                EquiposLaicon equipoLaiconVista = new EquiposLaicon();
                equipoLaiconVista.Id = equipoLaiconTesgestion.Id;
                equipoLaiconVista.LaiconId = equipoLaiconTesgestion.LaiconId;
                equipoLaiconVista.LocationCode = equipoLaiconTesgestion.LocationCode;
                equipoLaiconVista.ModelDescription = equipoLaiconTesgestion.ModelDescription;
                equipoLaiconVista.ModelIdPLU = equipoLaiconTesgestion.TGProyectos_Plu.Plu;
                equipoLaiconVista.Serial = equipoLaiconTesgestion.SerialFabricante;
                equipoLaiconVista.State = equipoLaiconTesgestion.State;
                equipoLaiconVista.IdEstacion = equipoLaiconTesgestion.IdEstacion;
                equiposLaiconVista.Add(equipoLaiconVista);
            }
            vistaLaicon.listaEquipos = equiposLaiconVista;
            return View(vistaLaicon);
        }

        /// <summary>
        /// retorna vista de filtrado por proyecto 
        /// </summary>
        [HttpPost]
        public ActionResult Laicon(vistaEquiposLaicon model)
        {
            List<TGProyectos_EquiposLaicon> equiposLaiconTesgestion = new List<TGProyectos_EquiposLaicon>();
            List<EquiposLaicon> equiposLaiconVista = new List<EquiposLaicon>();
            vistaEquiposLaicon vistaLaicon = new vistaEquiposLaicon();
            string tipo = model.Tipo;
            vistaLaicon.listaTiposEquipo = ObtenerListaTiposLaicon(tipo);
            using (TesGestionProyectosEntities dbContex = new TesGestionProyectosEntities())
            {
                equiposLaiconTesgestion = dbContex.TGProyectos_EquiposLaicon.Where(x => x.ElementType == tipo).ToList();

                foreach (TGProyectos_EquiposLaicon equipoLaiconTesgestion in equiposLaiconTesgestion)
                {
                    EquiposLaicon equipoLaiconVista = new EquiposLaicon();
                    equipoLaiconVista.Id = equipoLaiconTesgestion.Id;
                    equipoLaiconVista.LaiconId = equipoLaiconTesgestion.LaiconId;
                    equipoLaiconVista.LocationCode = equipoLaiconTesgestion.LocationCode;
                    equipoLaiconVista.ModelDescription = equipoLaiconTesgestion.ModelDescription;
                    equipoLaiconVista.ModelIdPLU = equipoLaiconTesgestion.TGProyectos_Plu.Plu;
                    equipoLaiconVista.Serial = equipoLaiconTesgestion.SerialFabricante;
                    equipoLaiconVista.State = equipoLaiconTesgestion.State;
                    equipoLaiconVista.IdEstacion = equipoLaiconTesgestion.IdEstacion;
                    equiposLaiconVista.Add(equipoLaiconVista);
                }
            }
            vistaLaicon.listaEquipos = equiposLaiconVista;
            return View(vistaLaicon);
        }
        public ActionResult cargarLaicon()
        {
            return PartialView("cargarLaicon");
        }
        [HttpPost]
        public ActionResult cargarLaicon(HttpPostedFileBase file)
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
                variablesSession.guardarIdProyecto(IdProyecto);
                return RedirectToAction("Laicon", "Equipos");
            }
            else
            {
                mensaje = "No se ha seleccionado ningun archivo valido";
                TempData["Mensaje"] = mensaje;
                variablesSession.guardarIdProyecto(IdProyecto);
                return RedirectToAction("Laicon", "Equipos");
            }
        }

        public ActionResult cargarCarroComprasNEC()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult cargarCarroComprasNEC(HttpPostedFileBase file)
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
                ReporteCargaArchivoModelo resultadoReporte = new ReporteCargaArchivoModelo();
                ValidadorEstructuraCarroComprasNEC leerCarroNEC = new ValidadorEstructuraCarroComprasNEC();
                string resultado = leerCarroNEC.validarFormatoExcelCCNEC(fileLocation);
                ////-----pasar de .xlsx a .xls----------------------------
                //string filexls = Server.MapPath("~/Content/") + "carritoNEC.xls";
                //Microsoft.Office.Interop.Excel.Application appExcel = new Microsoft.Office.Interop.Excel.Application();
                //appExcel.DisplayAlerts = false;
                //Microsoft.Office.Interop.Excel.Workbook ExcelDocument = appExcel.Workbooks.Open(fileLocation);
                //ExcelDocument.CheckCompatibility = false;
                //ExcelDocument.SaveAs(filexls, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8);
                //ExcelDocument.Close();
                //appExcel.Quit();
                ////----------Validar los nombres de las columnas---------------
                //Workbook reporteCarroNEC = Workbook.Load(filexls);
                //ReporteCargaArchivoModelo resultadoReporte = new ReporteCargaArchivoModelo();
                //ValidadorEstructuraCarroComprasNEC leerCarroNEC = new ValidadorEstructuraCarroComprasNEC();
                //string resultado = leerCarroNEC.CargarXLSCarroComprasNEC(reporteCarroNEC);
                //if (!string.IsNullOrWhiteSpace(resultado))
                //{
                //    resultadoReporte.Reporte = resultado;
                //    System.IO.File.Delete(fileLocation);
                //    System.IO.File.Delete(filexls);
                //    mensaje = "El archivo " + Request.Files["file"].FileName + " no tiene el formato de carrito de compras NEC correcto";
                //    TempData["Mensaje"] = mensaje;
                //    return View("Index", resultadoReporte);
                //}
                ////--------------Validar informacion-------------
                //ValidadorInformacionCarroComprasNEC validarInformacionCarroNEC = new ValidadorInformacionCarroComprasNEC(reporteCarroNEC);
                //string respuestaValidarInformacion = validarInformacionCarroNEC.CargarXLSCarroComprasNEC();
                //if (!string.IsNullOrWhiteSpace(respuestaValidarInformacion))
                //{
                //    resultadoReporte.Reporte = respuestaValidarInformacion;
                //    System.IO.File.Delete(fileLocation);
                //    System.IO.File.Delete(filexls);
                //    mensaje = "El archivo " + Request.Files["file"].FileName + " contiene información no valida";
                //    TempData["Mensaje"] = mensaje;
                //    return View("Index", resultadoReporte);
                //}
                ////---------guardar en bases de datos--------------------------
                //CargarCarroComprasNEC cargarCarroNECBD = new CargarCarroComprasNEC();
                //string resultadoCargaEquipos = cargarCarroNECBD.CargarCarroComprasNECBD(validarInformacionCarroNEC.Estructura);
                //if (!string.IsNullOrWhiteSpace(resultadoCargaEquipos))
                //{
                //    resultadoReporte.Reporte = resultadoCargaEquipos;
                //    System.IO.File.Delete(fileLocation);
                //    System.IO.File.Delete(filexls);
                //    mensaje = "El archivo " + Request.Files["file"].FileName + " contiene información valida";
                //    TempData["Mensaje"] = mensaje;
                //    return View("Index", resultadoReporte);
                //}
                if (!string.IsNullOrWhiteSpace(resultado))
                {
                    resultadoReporte.Reporte = resultado;
                    System.IO.File.Delete(fileLocation);
                    mensaje = "El archivo " + Request.Files["file"].FileName + "se termino de procesar";
                    TempData["Mensaje"] = mensaje;
                    return View("Index", resultadoReporte);
                }
                System.IO.File.Delete(fileLocation);
                //System.IO.File.Delete(filexls);
                mensaje = "El archivo " + Request.Files["file"].FileName + " se ha cargado correctamente";
                TempData["Mensaje"] = mensaje;
                return RedirectToAction("CarroComprasNEC", "Equipos");

            }
            else
            {
                mensaje = "No se ha seleccionado ningun archivo valido";
                TempData["Mensaje"] = mensaje;
                return RedirectToAction("CarroComprasNEC", "Equipos");
            }
        }
    }
}