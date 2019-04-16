using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExcelLibrary.SpreadSheet;
using System.Text;
using System.Globalization;
using ClosedXML.Excel;
using LinqToExcel;
using Proyectotransmilenio.Models;

namespace Proyectotransmilenio.CargarArchivos
{
    public class ValidadorEstructuraLaicon
    {        
        public ValidadorEstructuraLaicon()
        {
        }
        public string CargarXLSLaicon(Workbook miexcel)
        {
            StringBuilder resultadoReporte = new StringBuilder();
            Worksheet mihoja = miexcel.Worksheets[0]; //Suponemos solo 1 hoja
            CellCollection cells = mihoja.Cells;
            Dictionary<int, string> lista = new Dictionary<int, string>();

            lista = getListaNombresColumnasLaicon();
            //Comparar el row principal de los encabezados
            int x = 0;
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
        public Dictionary<int, string> getListaNombresColumnasLaicon()
        {
            Dictionary<int, string> respuesta = new Dictionary<int, string>();
            respuesta.Add(0, "Laicon Id");
            respuesta.Add(1, "Mic Code");
            respuesta.Add(2, "Model Id (PLU)");           
            respuesta.Add(3, "Model Description");
            respuesta.Add(4, "Maker");
            respuesta.Add(5, "Reference");
            respuesta.Add(6, "Sub-reference");
            respuesta.Add(7, "Value Type");
            respuesta.Add(8, "Is Asset?");
            respuesta.Add(9, "Plu");
            respuesta.Add(10, "Element Type");
            respuesta.Add(11, "Element Category");
            respuesta.Add(12, "Element Category Description");
            respuesta.Add(13, "Location Code");
            respuesta.Add(14, "Location Name");
            respuesta.Add(15, "Geo. Location Code");
            respuesta.Add(16, "Geo. Location Name");
            respuesta.Add(17, "Serial");
            respuesta.Add(18, "Purchase Price");
            respuesta.Add(19, "State");
            respuesta.Add(20, "Total Quantity");
            respuesta.Add(21, "Total Reserved");
            respuesta.Add(22, "Location Asigned Code");
            respuesta.Add(23, "Internal Location Code");
            respuesta.Add(24, "In Inventory?");
            respuesta.Add(25, "Is Spare Part?");
            respuesta.Add(26, "Is Deleted?");
            respuesta.Add(27, "Is Movement?");
            respuesta.Add(28, "Air Waybill Number");
            respuesta.Add(29, "Invoice Number");
            respuesta.Add(30, "Insurance Value");
            respuesta.Add(31, "Asset Type");
            respuesta.Add(32, "Asset Category");
            respuesta.Add(33, "Investment Item");
            respuesta.Add(34, "Asset Value (cop)");
            respuesta.Add(35, "Asset Value (usd)");
            respuesta.Add(36, "Asset Trm");
            respuesta.Add(37, "Nationalization Value (cop)");
            respuesta.Add(38, "Nationalization Value (usd)");
            respuesta.Add(39, "Nationalization Trm");
            respuesta.Add(40, "Purchase Order");
            respuesta.Add(41, "Purchase Order Line");
            respuesta.Add(42, "Fa Number (wip/temp)");
            respuesta.Add(43, "Fa Number");
            respuesta.Add(44, "Fa Sub-number");
            respuesta.Add(45, "Warehouse Entry");
            respuesta.Add(46, "Car");
            respuesta.Add(47, "Date Capitalization");
            respuesta.Add(48, "Piece, Box Or Stowage");
            respuesta.Add(49, "Classification");
            respuesta.Add(50, "Reenabled");
            respuesta.Add(51, "Containing Element");
            respuesta.Add(52, "Commentaries");
            respuesta.Add(53, "Inside");
            respuesta.Add(54, "Order");
            respuesta.Add(55, "Location Order");
            respuesta.Add(56, "Direction Order");
            respuesta.Add(57, "State Order");
            return respuesta;
        }
        /// <summary>
        /// Valida los encabezados de las columnas del excel con LinqtoExcel
        /// </summary>
        /// <param name="path">directorio del archivo excel</param>
        /// <returns></returns>
        public string validarFormatoExcelLaicon(string path)
        {
            string resultadoReporteTotal = "";
            string resultadoReporteHoja = "";
            string resultadoBD = "";
            Dictionary<int, string> lista = new Dictionary<int, string>();
            lista = getListaNombresColumnasLaicon();
            var excelLaicon = new ExcelQueryFactory(path);
            excelLaicon.ReadOnly = true;
            var worksheetNombres = excelLaicon.GetWorksheetNames();
            IEnumerable<string> columnNames = new List<string>();
            foreach (string nombreHoja in worksheetNombres)
            {
                columnNames = excelLaicon.GetColumnNames(nombreHoja);
                resultadoReporteHoja = compararEncabezados(columnNames, nombreHoja);
                if (string.IsNullOrEmpty(resultadoReporteHoja))
                {
                    resultadoBD = obtenerDatos(path, nombreHoja);
                }
                resultadoReporteTotal = resultadoReporteTotal + resultadoReporteHoja + resultadoBD;
            }
            return resultadoReporteTotal;
        }
        /// <summary>
        /// compara celdas del encabezado del excel con el formato almacenado
        /// </summary>
        /// <param name="columnNames">lista de los nombres de las columnas del excel</param>
        /// <param name="NombreHoja">nombre de la hoja</param>
        /// <returns></returns>
        public string compararEncabezados(IEnumerable<string> columnNames, string NombreHoja)
        {
            StringBuilder resultadoReporte = new StringBuilder();
            Dictionary<int, string> listaValida = new Dictionary<int, string>();
            listaValida = getListaNombresColumnasLaicon();
            int numeroColumnasFormatoValido = listaValida.Count();
            int numeroColumnasExcel = columnNames.Count();
            if (numeroColumnasFormatoValido > numeroColumnasExcel)
            {
                resultadoReporte.AppendLine(string.Format("El numero de columnas del archivo cargado es menor que el formato requerido en la hoja: " + NombreHoja));
            }
            else if (numeroColumnasFormatoValido < numeroColumnasExcel)
            {
                resultadoReporte.AppendLine(string.Format("El numero de columnas del archivo cargado es mayor que el formato requerido en la hoja: " + NombreHoja));
            }
            else if (numeroColumnasExcel == numeroColumnasFormatoValido)
            {
                foreach (var nombreColumna in listaValida)
                {
                    if (nombreColumna.Key == 15 || nombreColumna.Key == 16)//github.com/paulyoder/LinqToExcel/issues/63
                    {
                        string nombre = nombreColumna.Value.Replace('.', '#');
                        if (!HerramientasLectorExcel.Comparar(columnNames.ElementAt(nombreColumna.Key), nombre))
                        {
                            resultadoReporte.AppendLine(string.Format("El validador del formato de reporte ha fallado en la celda 0,{0} con el encabezado que se deberia llamar {1} en la hoja {2}", (nombreColumna.Key + 1), nombreColumna.Value, NombreHoja));
                        }
                    }
                    else
                    {
                        if (!HerramientasLectorExcel.Comparar(columnNames.ElementAt(nombreColumna.Key), nombreColumna.Value))
                        {
                            resultadoReporte.AppendLine(string.Format("El validador del formato de reporte ha fallado en la celda 0,{0} con el encabezado que se deberia llamar {1} en la hoja {2}", (nombreColumna.Key + 1), nombreColumna.Value, NombreHoja));
                        }
                    }
                }
            }
            return resultadoReporte.ToString();
        }
        /// <summary>
        /// carga a la herramienta la informacion de laicon para procesarla y almacenarla en BD
        /// </summary>
        /// <param name="path">directorio del archivo excel</param>
        /// <param name="NombreHoja">hoja</param>
        /// <returns></returns>
        public string obtenerDatos(string path, string NombreHoja)
        {
            StringBuilder resultadoReporte = new StringBuilder();
            string resultadoTotal = "";
            List<FilaLaicon> equiposExcel = new List<FilaLaicon>();
            var excelLaicon = new ExcelQueryFactory(path);
            excelLaicon.ReadOnly = true;
            var equipos = from equiposxls in excelLaicon.Worksheet<FilaLaicon>(NombreHoja)
                          select equiposxls;
            resultadoReporte.AppendLine(string.Format("se obtuvieron {0} equipos el la hoja {1}", equipos.Count(), NombreHoja));
            foreach (FilaLaicon equipo in equipos)
            {
                equiposExcel.Add(equipo);
            }
            CargarEquiposLaicon cargarEquiposLaiconBD = new CargarEquiposLaicon();
            //string resultadoCargaEquipos = cargarEquiposLaiconBD.CargarEquiposLaiconBDLinq(equiposExcel, NombreHoja);
            //resultadoTotal = resultadoReporte.ToString() + resultadoCargaEquipos;
            return resultadoTotal;
        }
    }
}