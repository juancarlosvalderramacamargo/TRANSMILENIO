using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExcelLibrary.SpreadSheet;
using System.Text;
using System.IO;
using Proyectotransmilenio.Models;
using System.Data.Entity;

namespace Proyectotransmilenio.CargarArchivos
{
    public class CargarVias
    {
        private IEnumerable<VIA> equiposLaiconTesgestion;
        private List<VIA> viasNuevas = new List<VIA>();
        private INFRAESTRUCTURA_TRANSMILENIOEntities db = new INFRAESTRUCTURA_TRANSMILENIOEntities();
        public CargarVias()
        {
            using (INFRAESTRUCTURA_TRANSMILENIOEntities db = new INFRAESTRUCTURA_TRANSMILENIOEntities())
            {
                equiposLaiconTesgestion = db.VIAS.ToList();
            }
        }
        public string CargarViasBD(EstructuraColumnasExcel tabla)
        {           
            StringBuilder resultadoReporte = new StringBuilder();
            foreach (List<ItemEstructura> item in tabla.TablaLaiconEstructura)
            {
                VIA viaExcel = new VIA();
                viaExcel.NOMBRE_VIA = (String)item[0].Elemento;
                VIA viaDB = new VIA();
                viaDB = equiposLaiconTesgestion.Where(x => x.NOMBRE_VIA == viaExcel.NOMBRE_VIA).FirstOrDefault();
                if (viaDB != null)
                {
                    resultadoReporte.AppendLine(string.Format("La via de la fila {0} ya existe en TRANSMILENIO", (item[0].RowIndex)));
                }
                else
                {
                    viaExcel.ID_VIA = db.VIAS.Max(x => x.ID_VIA) + 1m;

                    viasNuevas.Add(viaExcel);
                    resultadoReporte.AppendLine(string.Format("La via de la fila {0} se creo en TRANSMILENIO", (item[0].RowIndex)));
                }

            }
            using (INFRAESTRUCTURA_TRANSMILENIOEntities dbContex = new INFRAESTRUCTURA_TRANSMILENIOEntities())
            {
                foreach (VIA equipoNuevo in viasNuevas)
                {
                    dbContex.VIAS.Add(equipoNuevo);
                    dbContex.SaveChanges();
                }
            }
            return resultadoReporte.ToString();
        }

        /// <summary>
        /// Guarda en BD los equipos laicon cargados con linqtoexcel
        /// </summary>
        /// <param name="tabla">lista de equipos obtenidos del excel cargado</param>
        /// <returns></returns>
        public string CargarViasBDLinq(List<FilaVia> tabla, string NombreHoja)
        {
            StringBuilder resultadoReporte = new StringBuilder();
            int fila = 0;   
            foreach (FilaVia item in tabla)
            {
                fila++;

                VIA viaExcel = new VIA();
                viaExcel.NOMBRE_VIA = item.nombreVia;
                VIA viaBD = new VIA();
                viaBD = db.VIAS.Where(x => x.NOMBRE_VIA == viaExcel.NOMBRE_VIA).FirstOrDefault();
                if (viaBD != null)
                {
                    resultadoReporte.AppendLine(string.Format("La via de la fila {0} ya existe en TRANSMILENIO", fila));
                }
                else
                {
                    viaExcel.ID_VIA = db.VIAS.Max(x=> x.ID_VIA)+1m;
                    try
                    {
                        db.VIAS.Add(viaExcel);
                        db.SaveChanges();
                    }
                    catch(Exception ex)
                    {
                    }                    
                    viasNuevas.Add(viaExcel);
                    resultadoReporte.AppendLine(string.Format("La via de la fila {0} se creo en TRANSMILENIO", fila));
                }

            }
            resultadoReporte.AppendLine(string.Format("se obtuvieron {0} vias nuevas en la hoja {1}", viasNuevas.Count(), NombreHoja));
            int equiposBD = tabla.Count() - viasNuevas.Count();
            resultadoReporte.AppendLine(string.Format("la hoja {0} tiene {1} vias que ya estan en TRANSMILENIO ", NombreHoja, equiposBD));            
            return resultadoReporte.ToString();
        }

    }
}