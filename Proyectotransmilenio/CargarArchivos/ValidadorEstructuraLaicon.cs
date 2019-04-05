using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExcelLibrary.SpreadSheet;
using System.Text;
using System.Globalization;

namespace TesGestionProyectos.CargarArchivos
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
    }
}