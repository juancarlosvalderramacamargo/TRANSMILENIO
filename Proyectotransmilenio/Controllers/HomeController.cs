using Proyectotransmilenio.Models;
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
            //var path = Server.MapPath("Content/Images/") + "modelER.jpeg";
            //byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            //string imageBase64Data = Convert.ToBase64String(imageByteData);
            //string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
            //return new FileContentResult(imageByteData, "image/jpeg");
            return View();
        }

        public ActionResult GenerarDatos()
        {
            GenerarDatos infoGenerar = new GenerarDatos();

            List<SelectListItem> listaTablas = new List<SelectListItem>();

            listaTablas.Add(new SelectListItem() { Value = "TIPOS_RUTAS", Text = "TIPOS_RUTAS" });
            listaTablas.Add(new SelectListItem() { Value = "TIPOS_BUSES", Text = "TIPOS_BUSES" });
            listaTablas.Add(new SelectListItem() { Value = "TIPOS_PARADEROS", Text = "TIPOS_PARADEROS" });
            listaTablas.Add(new SelectListItem() { Value = "BUSES", Text = "BUSES" });
            listaTablas.Add(new SelectListItem() { Value = "VIAS", Text = "VIAS" });
            listaTablas.Add(new SelectListItem() { Value = "RUTAS", Text = "RUTAS" });
            listaTablas.Add(new SelectListItem() { Value = "PARADEROS", Text = "PARADEROS" });

            infoGenerar.ListaTablas = listaTablas;

            return View(infoGenerar);
        }

        [HttpPost]
        public ActionResult GenerarDatos(GenerarDatos model)
        {
            INFRAESTRUCTURA_TRANSMILENIOEntities db = new INFRAESTRUCTURA_TRANSMILENIOEntities();

            if (model.NumeroRegistros > 0)
            {
                switch (model.NombreTabla)
                {
                    case "TIPOS_RUTAS":
                        TIPOS_RUTAS nuevoTipRuta = new TIPOS_RUTAS();
                        decimal idTipoRuta = db.TIPOS_RUTAS.Max(y => y.ID_TIPO_RUTA);

                        for (int i = 1; i <= model.NumeroRegistros; i++)
                        {
                            idTipoRuta = idTipoRuta + 1;
                            nuevoTipRuta = new TIPOS_RUTAS();
                            nuevoTipRuta.ID_TIPO_RUTA = idTipoRuta;
                            nuevoTipRuta.TIPO_RUTA = "Tipo Ruta " + i;

                            db.TIPOS_RUTAS.Add(nuevoTipRuta);
                        }
                        break;

                    case "TIPOS_BUSES":
                        TIPOS_BUSES nuevoTipBus = new TIPOS_BUSES();
                        decimal idTipoBus = db.TIPOS_BUSES.Max(y => y.ID_TIPO_BUS);

                        for (int i = 1; i <= model.NumeroRegistros; i++)
                        {
                            idTipoBus = idTipoBus + 1;
                            nuevoTipBus = new TIPOS_BUSES();
                            nuevoTipBus.ID_TIPO_BUS = idTipoBus;
                            nuevoTipBus.TIPO_BUS = "Tipo Bus " + i;

                            db.TIPOS_BUSES.Add(nuevoTipBus);
                        }
                        break;

                    case "TIPOS_PARADEROS":
                        TIPOS_PARADEROS nuevoTipPar = new TIPOS_PARADEROS();
                        decimal idTipoPar = db.TIPOS_PARADEROS.Max(y => y.ID_TIPO_PAR);

                        for (int i = 1; i <= model.NumeroRegistros; i++)
                        {
                            idTipoPar = idTipoPar + 1;
                            nuevoTipPar = new TIPOS_PARADEROS();
                            nuevoTipPar.ID_TIPO_PAR = idTipoPar;
                            nuevoTipPar.TIPO_PARADERO = "Tipo Paradero " + i;

                            db.TIPOS_PARADEROS.Add(nuevoTipPar);
                        }
                        break;

                    case "BUSES":
                        Bus nuevoBus = new Bus();
                        decimal idBus = db.BUSES.Max(y => y.ID_BUS);

                        for (int i = 1; i <= model.NumeroRegistros; i++)
                        {
                            idBus = idBus + 1;
                            nuevoBus = new Bus();
                            nuevoBus.ID_BUS = idBus;
                            nuevoBus.ID_TIPO_BUS = 1;
                            nuevoBus.MARCA = "Marca " + i;
                            nuevoBus.MODELO = "Modelo " + i;
                            nuevoBus.CONDUTOR = "Conductor " + i;
                            nuevoBus.COLOR = "Color " + i;

                            db.BUSES.Add(nuevoBus);
                        }
                        break;

                    case "VIAS":
                        VIA nuevoVia = new VIA();
                        decimal idVuia = db.VIAS.Max(y => y.ID_VIA);

                        for (int i = 1; i <= model.NumeroRegistros; i++)
                        {
                            idVuia = idVuia + 1;
                            nuevoVia = new VIA();
                            nuevoVia.ID_VIA = idVuia;
                            nuevoVia.NOMBRE_VIA = "Via " + i;

                            db.VIAS.Add(nuevoVia);
                        }
                        break;

                    case "RUTAS":
                        RUTA nuevoRuta = new RUTA();
                        decimal idRuta = db.RUTAS.Max(y => y.ID_RUTA);

                        for (int i = 1; i <= model.NumeroRegistros; i++)
                        {
                            idRuta = idRuta + 1;
                            nuevoRuta = new RUTA();
                            nuevoRuta.ID_RUTA = idRuta;
                            nuevoRuta.ID_BUS = 1;
                            nuevoRuta.ID_TIPO_RUTA = 1;
                            nuevoRuta.NOMBRE_RUTA = "Ruta " + i;

                            db.RUTAS.Add(nuevoRuta);
                        }
                        break;

                    case "PARADEROS":
                        PARADERO nuevoParadero = new PARADERO();
                        decimal idParadero = db.PARADEROS.Max(y => y.ID_PARADERO);

                        for (int i = 1; i <= model.NumeroRegistros; i++)
                        {
                            idParadero = idParadero + 1;
                            nuevoParadero = new PARADERO();
                            nuevoParadero.ID_PARADERO = idParadero;
                            nuevoParadero.ID_TIPO_PAR = 1;
                            nuevoParadero.ID_VIA = 1;
                            nuevoParadero.NOMBRE_PARADERO = "Paradero " + i;
                            nuevoParadero.UBICACION_PARADERO = "(X = " + i + "; Y = " + i + ")";
                            nuevoParadero.POSICION_VIA = i;

                            db.PARADEROS.Add(nuevoParadero);
                        }
                        break;
                }

                db.SaveChanges();
                db.Dispose();
            }

            return RedirectToAction("GenerarDatos", "Home");
        }
    }
}