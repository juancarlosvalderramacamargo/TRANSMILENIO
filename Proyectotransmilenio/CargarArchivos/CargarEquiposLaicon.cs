using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExcelLibrary.SpreadSheet;
using System.Text;
using System.IO;
using Proyectotransmilenio.Models;

namespace Proyectotransmilenio.CargarArchivos
{
    public class CargarEquiposLaicon
    {
        //private IEnumerable<TGProyectos_EquiposLaicon> equiposLaiconTesgestion;
        //private List<TGProyectos_EquiposLaicon> equipoLaiconNuevos = new List<TGProyectos_EquiposLaicon>();
        public CargarEquiposLaicon()
        {
            //using (TesGestionProyectosEntities dbContex = new TesGestionProyectosEntities())
            //{
            //    equiposLaiconTesgestion = dbContex.TGProyectos_EquiposLaicon.ToList();
            //}
        }
        public string CargarEquiposLaiconBD(EstructuraColumnasExcel tabla)
        {           
            StringBuilder resultadoReporte = new StringBuilder();            
            //foreach (List<ItemEstructura> item in tabla.TablaLaiconEstructura)
            //{
            //    TGProyectos_EquiposLaicon equipoLaicon = new TGProyectos_EquiposLaicon();                
            //    equipoLaicon.LaiconId = (Int64)item[0].Elemento;
            //    TGProyectos_EquiposLaicon equipoLaiconExistente = new TGProyectos_EquiposLaicon();
            //    equipoLaiconExistente = equiposLaiconTesgestion.Where(x => x.LaiconId == equipoLaicon.LaiconId).FirstOrDefault();
            //    if (equipoLaiconExistente != null)
            //    {
            //        resultadoReporte.AppendLine(string.Format("El equipo de la fila {0} ya existe en TESGEStion WEB", (item[0].RowIndex)));
            //    }
            //    else
            //    {
            //        equipoLaicon.Id = Guid.NewGuid();
            //        equipoLaicon.LocationCode = (string)item[13].Elemento;
            //        equipoLaicon.ModelDescription = (string)item[3].Elemento;
            //        equipoLaicon.ModelIdPLU = (Int64)item[2].Elemento;
            //        equipoLaicon.SerialFabricante = (string)item[17].Elemento;
            //        equipoLaicon.State = (string)item[19].Elemento;
            //        equipoLaicon.AssetType = (string)item[31].Elemento; ;

            //        equipoLaiconNuevos.Add(equipoLaicon);
            //        resultadoReporte.AppendLine(string.Format("El equipo de la fila {0} se creo en TESGEStion WEB", (item[0].RowIndex)));
            //    }

            //}
            //using (TesGestionProyectosEntities dbContex = new TesGestionProyectosEntities())
            //{
            //    foreach (TGProyectos_EquiposLaicon equipoNuevo in equipoLaiconNuevos)
            //    {
            //        dbContex.TGProyectos_EquiposLaicon.Add(equipoNuevo);
            //        dbContex.SaveChanges();                   
            //    }
            //}
                return resultadoReporte.ToString();
        }
    }
}