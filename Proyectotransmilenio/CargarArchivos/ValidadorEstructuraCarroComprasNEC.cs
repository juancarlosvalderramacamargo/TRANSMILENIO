using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExcelLibrary.SpreadSheet;
using System.Text;
using System.Globalization;

namespace TesGestionProyectos.CargarArchivos
{
    public class ValidadorEstructuraCarroComprasNEC
    {
        public ValidadorEstructuraCarroComprasNEC()
        {
        }
        public string CargarXLSCarroComprasNEC(Workbook miexcel)
        {
            StringBuilder resultadoReporte = new StringBuilder();
            Worksheet mihoja = miexcel.Worksheets[0]; //Suponemos solo 1 hoja
            CellCollection cells = mihoja.Cells;
            Dictionary<int, string> lista = new Dictionary<int, string>();
            string celdaContrato = cells[0, 1].ToString().ToUpper();
            if (!celdaContrato.Contains("CONTRATO"))
            {
                resultadoReporte.AppendLine(string.Format("El validador del formato de reporte ha fallado en la celda {0},{1} con el encabezado que se deberia contener la palabra {2}", 1, 2, "CONTRATO"));
            }
            lista = getListaNombresColumnasCarroComprasNEC();
            //Comparar el row principal de los encabezados
            int x = 1;
            foreach (var item in lista)
            {
                if (!HerramientasLectorExcel.Comparar(cells[(x), (item.Key)].ToString(), item.Value))
                {
                    resultadoReporte.AppendLine(string.Format("El validador del formato de reporte ha fallado en la celda {0},{1} con el encabezado que se deberia llamar {2}", (x), (item.Key + 1), item.Value));
                }
            }
            return resultadoReporte.ToString();
        }
        /// <summary>
        /// Nombres del encabezado de las columnas del reporte LAICON
        /// </summary>
        /// <returns>Dicionario con numero y nombre de las columnas del reporte de LAICON</returns>
        public Dictionary<int, string> getListaNombresColumnasCarroComprasNEC()
        {
            Dictionary<int, string> respuesta = new Dictionary<int, string>();
            respuesta.Add(0, "");
            respuesta.Add(1, "LINE DESCRIPTION");
            respuesta.Add(2, "V/U sin IVA");
            respuesta.Add(3, "LINE");            
            return respuesta;
        }
    }
}


