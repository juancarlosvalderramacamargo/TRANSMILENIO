using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExcelLibrary.SpreadSheet;
using System.Text;
using System.Globalization;
using ClosedXML.Excel;
using Proyectotransmilenio.Models;

namespace ProyectoTransmilenio.CargarArchivos
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

        /// <summary>
        /// Valida los encabezados de las columnas del excel con closedXML
        /// </summary>
        /// <param name="path">directorio del archivo excel</param>
        /// <returns></returns>
        public string validarFormatoExcelCCNEC(string path)
        {
            string resultadoReporteTotal = "";
            string resultadoReporteHoja = "";
            string resultadoBD = "";
            XLWorkbook wb = new XLWorkbook(path);
            IXLWorksheet ws = wb.Worksheets.First();
            IXLRow rowtitulos = ws.Row(2);
            List<string> columnNames = obtenerEncabezadosNEC(rowtitulos);
            resultadoReporteHoja = compararEncabezados(columnNames);
            if (string.IsNullOrEmpty(resultadoReporteHoja))
            {
                resultadoBD = obtenerDatos(ws);
            }
            resultadoReporteTotal = resultadoReporteTotal + resultadoReporteHoja + resultadoBD;

            return resultadoReporteTotal;
        }

        public List<string> obtenerEncabezadosNEC(IXLRow rowTitulos)
        {
            List<string> columnasExcel = new List<string>();
            string datoCelda = "";
            columnasExcel.Add("");
            foreach (IXLCell cell in rowTitulos.Cells())
            {
                datoCelda = string.IsNullOrWhiteSpace(cell.Value.ToString()) ? "" : cell.Value.ToString();
                columnasExcel.Add(datoCelda);
            }
            return columnasExcel;
        }

        /// <summary>
        /// compara celdas del encabezado del excel con el formato almacenado
        /// </summary>
        /// <param name="columnNames">lista de los nombres de las columnas del excel</param>
        /// <param name="NombreHoja">nombre de la hoja</param>
        /// <returns></returns>
        public string compararEncabezados(List<string> columnNames)
        {
            StringBuilder resultadoReporte = new StringBuilder();
            Dictionary<int, string> listaValida = new Dictionary<int, string>();
            listaValida = getListaNombresColumnasCarroComprasNEC();
            int numeroColumnasFormatoValido = listaValida.Count();
            int numeroColumnasExcel = columnNames.Count();
            if (numeroColumnasFormatoValido > numeroColumnasExcel)
            {
                resultadoReporte.AppendLine(string.Format("El numero de columnas del archivo cargado es menor que el formato requerido"));
            }
            else if (numeroColumnasFormatoValido < numeroColumnasExcel)
            {
                resultadoReporte.AppendLine(string.Format("El numero de columnas del archivo cargado es mayor que el formato requerido"));
            }
            else if (numeroColumnasExcel == numeroColumnasFormatoValido)
            {
                foreach (var nombreColumna in listaValida)
                {
                    if (!HerramientasLectorExcel.Comparar(columnNames.ElementAt(nombreColumna.Key), nombreColumna.Value))
                    {
                        resultadoReporte.AppendLine(string.Format("El validador del formato de reporte ha fallado en la celda 0,{0} con el encabezado que se deberia llamar {1}", (nombreColumna.Key + 1), nombreColumna.Value));
                    }
                }
            }
            return resultadoReporte.ToString();
        }
        /// <summary>
        /// carga a la herramienta la informacion de laicon para procesarla y almacenarla en BD
        /// </summary>
        /// <param name="LobjFullRange">excel cargado con closedxml</param>
        /// <returns></returns>
        public string obtenerDatos(IXLWorksheet ws)
        {
            StringBuilder resultadoReporte = new StringBuilder();
            string resultadoTotal = "";
            List<FilaCarritoNEC> equiposExcel = new List<FilaCarritoNEC>();
            string contrato = string.IsNullOrWhiteSpace(ws.Row(1).Cell(2).Value.ToString()) ? "" : ws.Row(1).Cell(2).Value.ToString();
            contrato = contrato.Trim().Replace("CONTRATO", "");
            //for (int i = 3; i <= LobjFullRange.RowCount(); i++)
            //{
            //    FilaCarritoNEC equipoCarrito = new FilaCarritoNEC();
            //    equipoCarrito.contrato = contrato;
            //    equipoCarrito.lineDescription = string.IsNullOrWhiteSpace(LobjFullRange.Cell(i, 1).GetString()) ? "" : LobjFullRange.Cell(i, 1).GetString();
            //    equipoCarrito.valor = string.IsNullOrWhiteSpace(LobjFullRange.Cell(i, 2).GetString()) ? "" : LobjFullRange.Cell(i, 2).GetString();
            //    equipoCarrito.line = string.IsNullOrWhiteSpace(LobjFullRange.Cell(i, 3).GetString()) ? "" : LobjFullRange.Cell(i, 3).GetString();
            //    equipoCarrito.plu1 = string.IsNullOrWhiteSpace(LobjFullRange.Cell(i, 4).GetString()) ? "" : LobjFullRange.Cell(i, 4).GetString();
            //    equipoCarrito.plu2 = string.IsNullOrWhiteSpace(LobjFullRange.Cell(i, 5).GetString()) ? "" : LobjFullRange.Cell(i, 5).GetString();
            //    equipoCarrito.plu3 = string.IsNullOrWhiteSpace(LobjFullRange.Cell(i, 6).GetString()) ? "" : LobjFullRange.Cell(i, 6).GetString();
            //    equipoCarrito.plu4 = string.IsNullOrWhiteSpace(LobjFullRange.Cell(i, 7).GetString()) ? "" : LobjFullRange.Cell(i, 7).GetString();
            //    equipoCarrito.plu5 = string.IsNullOrWhiteSpace(LobjFullRange.Cell(i, 8).GetString()) ? "" : LobjFullRange.Cell(i, 8).GetString();
            //    equipoCarrito.plu6 = string.IsNullOrWhiteSpace(LobjFullRange.Cell(i, 9).GetString()) ? "" : LobjFullRange.Cell(i, 9).GetString();
            //    equipoCarrito.plu7 = string.IsNullOrWhiteSpace(LobjFullRange.Cell(i, 10).GetString()) ? "" : LobjFullRange.Cell(i, 10).GetString();
            //    equiposExcel.Add(equipoCarrito);

            //}
            for (int i = 3; i <= ws.RangeUsed().RowCount(); i++)
            {
                IXLRow row = ws.Row(i);
                FilaCarritoNEC equipoCarrito = new FilaCarritoNEC();
                equipoCarrito.contrato = contrato;
                equipoCarrito.lineDescription = string.IsNullOrWhiteSpace(row.Cell(2).GetString()) ? "" : row.Cell(2).GetString();
                equipoCarrito.valor = string.IsNullOrWhiteSpace(row.Cell(3).GetString()) ? "" : row.Cell(3).GetString();
                equipoCarrito.line = string.IsNullOrWhiteSpace(row.Cell(4).GetString()) ? "" : row.Cell(4).GetString();
                equipoCarrito.plu1 = string.IsNullOrWhiteSpace(row.Cell(5).GetString()) ? "" : row.Cell(5).GetString();
                equipoCarrito.plu2 = string.IsNullOrWhiteSpace(row.Cell(6).Value.ToString()) ? "" : row.Cell(6).Value.ToString();
                equipoCarrito.plu3 = string.IsNullOrWhiteSpace(row.Cell(7).Value.ToString()) ? "" : row.Cell(7).Value.ToString();
                equipoCarrito.plu4 = string.IsNullOrWhiteSpace(row.Cell(8).Value.ToString()) ? "" : row.Cell(8).Value.ToString();
                equipoCarrito.plu5 = string.IsNullOrWhiteSpace(row.Cell(9).Value.ToString()) ? "" : row.Cell(9).Value.ToString();
                equipoCarrito.plu6 = string.IsNullOrWhiteSpace(row.Cell(10).Value.ToString()) ? "" : row.Cell(10).Value.ToString();
                equipoCarrito.plu7 = string.IsNullOrWhiteSpace(row.Cell(11).Value.ToString()) ? "" : row.Cell(11).Value.ToString();
                equiposExcel.Add(equipoCarrito);

            }
            resultadoReporte.AppendLine(string.Format("se obtuvieron {0} equipos", equiposExcel.Count()));

            CargarCarroComprasNEC cargarCarritoNEcBD = new CargarCarroComprasNEC();
            //string resultadoCargaEquipos = cargarCarritoNEcBD.CargarCarritoComprasNEC(equiposExcel);
            //resultadoTotal = resultadoReporte.ToString() + resultadoCargaEquipos;
            return resultadoTotal;
        }
    }
}


