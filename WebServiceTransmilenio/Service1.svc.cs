using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WebServiceTransmilenio
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string SincronizarBD()
        {
            TRANSMILENIOEntities db = new TRANSMILENIOEntities();
            INFRAESTRUCTURA_TRANSMILENIOEntities dbContext = new INFRAESTRUCTURA_TRANSMILENIOEntities();
            string respuesta = "";
            decimal idBus = 0, idVia = 0, idTipoBus = 0, idTipoRuta = 0, idRuta = 0, idTipoParadero = 0, idParadero = 0, idRutaParadero = 0;

            try
            {
                List<TRANSMILENIO> infoBD1 = db.TRANSMILENIO.ToList();

                foreach (TRANSMILENIO info in infoBD1)
                {
                    if (!dbContext.VIAS.Where(x => x.NOMBRE_VIA.ToUpper() == info.NOMBRE_VIA.ToUpper()).Any())
                    {
                        idVia = dbContext.VIAS.Max(y => y.ID_VIA);

                        VIAS nuevaVia = new VIAS();
                        nuevaVia.ID_VIA = idVia + 1;
                        nuevaVia.NOMBRE_VIA = info.NOMBRE_VIA;
                        dbContext.VIAS.Add(nuevaVia);
                        dbContext.SaveChanges();
                    }

                    if (!dbContext.TIPOS_BUSES.Where(x => x.TIPO_BUS.ToUpper() == info.TIPO_BUS.ToUpper()).Any())
                    {
                        idTipoBus = dbContext.TIPOS_BUSES.Max(y => y.ID_TIPO_BUS) + 1;

                        TIPOS_BUSES nuevoTipoBus = new TIPOS_BUSES();
                        nuevoTipoBus.ID_TIPO_BUS = idTipoBus;
                        nuevoTipoBus.TIPO_BUS = info.TIPO_BUS;
                    }
                    else
                    {
                        idTipoBus = dbContext.TIPOS_BUSES.Where(x => x.TIPO_BUS.ToUpper() == info.TIPO_BUS.ToUpper()).FirstOrDefault().ID_TIPO_BUS;
                    }

                    idBus = dbContext.BUSES.Max(y => y.ID_BUS) + 1;

                    BUSES nuevoBus = new BUSES();
                    nuevoBus.ID_BUS = idBus;
                    nuevoBus.ID_TIPO_BUS = idTipoBus;
                    nuevoBus.MARCA = info.MARCA;
                    nuevoBus.MODELO = info.MODELO;
                    nuevoBus.CONDUTOR = info.CONDUTOR;
                    nuevoBus.COLOR = info.COLOR;

                    if (!dbContext.TIPOS_RUTAS.Where(x => x.TIPO_RUTA.ToUpper() == info.TIPO_RUTA.ToUpper()).Any())
                    {
                        idTipoRuta = dbContext.TIPOS_RUTAS.Max(y => y.ID_TIPO_RUTA) + 1;

                        TIPOS_RUTAS nuevoTipoRuta = new TIPOS_RUTAS();
                        nuevoTipoRuta.ID_TIPO_RUTA = idTipoRuta;
                        nuevoTipoRuta.TIPO_RUTA = info.TIPO_RUTA;
                    }
                    else
                    {
                        idTipoRuta = dbContext.TIPOS_RUTAS.Where(x => x.TIPO_RUTA.ToUpper() == info.TIPO_RUTA.ToUpper()).FirstOrDefault().ID_TIPO_RUTA;
                    }

                    if (!dbContext.RUTAS.Where(x => x.NOMBRE_RUTA.ToUpper() == info.NOMBRE_RUTA.ToUpper()).Any())
                    {
                        idRuta = dbContext.RUTAS.Max(y => y.ID_RUTA);

                        RUTAS nuevaRuta = new RUTAS();
                        nuevaRuta.ID_BUS = idBus;
                        nuevaRuta.ID_RUTA = idRuta;
                        nuevaRuta.ID_TIPO_RUTA = idTipoRuta;
                        nuevaRuta.NOMBRE_RUTA = info.NOMBRE_RUTA;
                        dbContext.RUTAS.Add(nuevaRuta);
                        dbContext.SaveChanges();
                    }

                    if (!dbContext.TIPOS_PARADEROS.Where(x => x.TIPO_PARADERO.ToUpper() == info.TIPO_PARADERO.ToUpper()).Any())
                    {
                        idTipoParadero = dbContext.TIPOS_PARADEROS.Max(y => y.ID_TIPO_PAR) + 1;

                        TIPOS_PARADEROS nuevoTipoBus = new TIPOS_PARADEROS();
                        nuevoTipoBus.ID_TIPO_PAR = idTipoParadero;
                        nuevoTipoBus.TIPO_PARADERO = info.TIPO_PARADERO;
                    }
                    else
                    {
                        idTipoParadero = dbContext.TIPOS_PARADEROS.Where(x => x.TIPO_PARADERO.ToUpper() == info.TIPO_PARADERO.ToUpper()).FirstOrDefault().ID_TIPO_PAR;
                    }

                    if (!dbContext.PARADEROS.Where(x => x.NOMBRE_PARADERO.ToUpper() == info.NOMBRE_PARADERO.ToUpper()).Any())
                    {
                        idParadero = dbContext.PARADEROS.Max(y => y.ID_PARADERO);

                        PARADEROS nuevoParadero = new PARADEROS();
                        nuevoParadero.ID_PARADERO = idParadero;
                        nuevoParadero.ID_TIPO_PAR = idTipoParadero;
                        nuevoParadero.ID_VIA = idVia;
                        nuevoParadero.NOMBRE_PARADERO = info.NOMBRE_PARADERO;
                        nuevoParadero.UBICACION_PARADERO = info.UBICACION_PARADERO;
                        nuevoParadero.POSICION_VIA = info.POSICION_VIA;
                        dbContext.PARADEROS.Add(nuevoParadero);
                        dbContext.SaveChanges();

                        RUTAS_PARADEROS rutaParadero = new RUTAS_PARADEROS();
                        rutaParadero.ID_PARADERO = idParadero;
                        rutaParadero.ID_RUTA = idRuta;
                        rutaParadero.POSICION_RUTA = info.POSICION_RUTA;
                        dbContext.RUTAS_PARADEROS.Add(rutaParadero);
                        dbContext.SaveChanges();
                    }
                }

                respuesta = "Se realizó la carga de información correctamente.";
            }
            catch
            {
                respuesta = "Se presento un error.";
            }

            return respuesta;
        }
    }
}
