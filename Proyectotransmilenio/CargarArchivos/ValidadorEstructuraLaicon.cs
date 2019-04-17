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
    public class ValidadorEstructuraVias
    {        
        public ValidadorEstructuraVias()
        {
        }
        public string CargarXLSVias(Workbook miexcel)
        {
            StringBuilder resultadoReporte = new StringBuilder();
            Worksheet mihoja = miexcel.Worksheets[0]; //Suponemos solo 1 hoja
            CellCollection cells = mihoja.Cells;
            Dictionary<int, string> lista = new Dictionary<int, string>();

            lista = getListaNombresColumnasVias();
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
        public Dictionary<int, string> getListaNombresColumnasVias()
        {
            Dictionary<int, string> respuesta = new Dictionary<int, string>();
            respuesta.Add(0, "NOMBRE VIA");          
            return respuesta;
        }
        /// <summary>
        /// Valida los encabezados de las columnas del excel con LinqtoExcel
        /// </summary>
        /// <param name="path">directorio del archivo excel</param>
        /// <returns></returns>
        public string validarFormatoExcelVias(string path)
        {
            string resultadoReporteTotal = "";
            string resultadoReporteHoja = "";
            string resultadoBD = "";
            Dictionary<int, string> lista = new Dictionary<int, string>();
            lista = getListaNombresColumnasVias();
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
            listaValida = getListaNombresColumnasVias();
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
            List<FilaVia> equiposExcel = new List<FilaVia>();
            var excelLaicon = new ExcelQueryFactory(path);
            excelLaicon.ReadOnly = true;
            var vias = from equiposxls in excelLaicon.Worksheet<FilaVia>(NombreHoja)
                          select equiposxls;
            resultadoReporte.AppendLine(string.Format("se obtuvieron {0} equipos el la hoja {1}", vias.Count(), NombreHoja));
            foreach (FilaVia via in vias)
            {
                equiposExcel.Add(via);
            }
            CargarVias cargarViasBD = new CargarVias();
            string resultadoCargaVias = cargarViasBD.CargarViasBDLinq(equiposExcel, NombreHoja);
            resultadoTotal = resultadoReporte.ToString() + resultadoCargaVias;
            return resultadoTotal;
        }
    }
}