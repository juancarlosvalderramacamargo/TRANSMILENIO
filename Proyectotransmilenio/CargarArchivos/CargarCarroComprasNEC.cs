using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExcelLibrary.SpreadSheet;
using System.Text;
using System.IO;
using TesGestionProyectos.Models;

namespace TesGestionProyectos.CargarArchivos
{
    public class CargarCarroComprasNEC
    {
        private IEnumerable<TGProyectos_CarritoComprasNEC> equiposCarroNECTesgestion;
        private List<TGProyectos_CarritoComprasNEC> equipoCarroNECNuevos = new List<TGProyectos_CarritoComprasNEC>();
        public CargarCarroComprasNEC()
        {
            using (TesGestionProyectosEntities dbContex = new TesGestionProyectosEntities())
            {
                equiposCarroNECTesgestion = dbContex.TGProyectos_CarritoComprasNEC.ToList();
            }
        }
        public string CargarCarroComprasNECBD(EstructuraColumnasExcel tabla)
        {
            StringBuilder resultadoReporte = new StringBuilder();
            foreach (List<ItemEstructura> item in tabla.TablaCarroNECEstructura)
            {
                TGProyectos_CarritoComprasNEC equipoCarroNEC = new TGProyectos_CarritoComprasNEC();
                equipoCarroNEC.LineDescription= (string)item[0].Elemento;
                equipoCarroNEC.Line = (int)item[2].Elemento;
                equipoCarroNEC.ValorUnitarioSinIVA = (decimal)item[1].Elemento;
                equipoCarroNEC.Contrato = (Int64)item[3].Elemento;
                TGProyectos_CarritoComprasNEC equipoCarroNECExistente = new TGProyectos_CarritoComprasNEC();
                equipoCarroNECExistente = equiposCarroNECTesgestion.Where(x => x.LineDescription == equipoCarroNEC.LineDescription && x.Line == equipoCarroNEC.Line && x.Contrato == equipoCarroNEC.Contrato).FirstOrDefault();
                if (equipoCarroNECExistente != null)
                {
                    resultadoReporte.AppendLine(string.Format("El equipo de la fila {0} ya existe en TESGEStion WEB", (item[0].RowIndex)));
                }
                else
                {
                    equipoCarroNEC.Id = Guid.NewGuid();
                    equipoCarroNECNuevos.Add(equipoCarroNEC);
                    resultadoReporte.AppendLine(string.Format("El equipo de la fila {0} se creo en TESGEStion WEB", (item[0].RowIndex)));
                }

            }
            using (TesGestionProyectosEntities dbContex = new TesGestionProyectosEntities())
            {
                foreach (TGProyectos_CarritoComprasNEC equipoNuevo in equipoCarroNECNuevos)
                {
                    dbContex.TGProyectos_CarritoComprasNEC.Add(equipoNuevo);
                    dbContex.SaveChanges();
                }
            }
            return resultadoReporte.ToString();
        }
    }
}
