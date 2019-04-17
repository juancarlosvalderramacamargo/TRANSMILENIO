using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExcelLibrary.SpreadSheet;
using System.Text;
using System.IO;

namespace Proyectotransmilenio.CargarArchivos
{
    public class ValidadorInformacionLaicon
    {
        private Workbook _miexcel;
        EstructuraColumnasExcel _estructura;
        Dictionary<int, NombreTiposColumnas> _listaTiposTablaLaicon;
        HerramientasLectorExcel validar = new HerramientasLectorExcel();

        public EstructuraColumnasExcel Estructura
        {
            get { return _estructura; }
            set { _estructura = value; }
        }

        public ValidadorInformacionLaicon(Workbook miexcel)
        {
            _miexcel = miexcel;
            _estructura = new EstructuraColumnasExcel();
            ConstruirListaTiposGrillaPrincipalColumnas();
        }

        public string CargarXLSLaicon()
        {           
            Worksheet mihoja = _miexcel.Worksheets[0];//suponemos solo 1 hoja
            CellCollection cells = mihoja.Cells;

            for (int rowIndex = mihoja.Cells.FirstRowIndex+1; rowIndex <= mihoja.Cells.LastRowIndex; rowIndex++)
            {
                List<ItemEstructura> rowTablaPrincipal = new List<ItemEstructura>();

                Row row = mihoja.Cells.GetRow(rowIndex);
                for (int colIndex = mihoja.Cells.FirstColIndex; colIndex <= mihoja.Cells.LastColIndex; colIndex++)
                {
                    Cell cell = row.GetCell(colIndex);
                    ItemEstructura nuevoItem = null;
                    nuevoItem = CrearItemTablaPrincipal(cell.Value, rowIndex, colIndex);
                    rowTablaPrincipal.Add(nuevoItem);
                }
                if (!validar.EsRowVacio(rowTablaPrincipal))
                {
                    _estructura.TablaLaiconEstructura.Add(rowTablaPrincipal);
                }

            }

            return GenerarReportePuntoAPuntoEliminar();
        }

        public string GenerarReportePuntoAPuntoEliminar()
        {
            StringBuilder resultadoReporte = new StringBuilder();

            if (_estructura.TablaLaiconEstructura.Count() == 0)
            {
                resultadoReporte.AppendLine("El validador de Información ha fallado, el formato de reporte LAICON no esta diligenciado");
            }

            foreach (List<ItemEstructura> lista in _estructura.TablaLaiconEstructura)
            {
                foreach (ItemEstructura item in lista)
                {
                    if (item.ErrorParcer)
                    {
                        resultadoReporte.AppendLine(string.Format("El validador de Información ha fallado en la celda Fila {0},Columna {1} el dato deberia ser de tipo {2}", (item.RowIndex), (item.ColIndex), item.Tipo.ToString()));
                    }
                    if (item.ErrorObligatorio)
                    {
                        resultadoReporte.AppendLine(string.Format("El validador de Información ha fallado en la celda Fila {0},Columna {1} el valor es obligatorio", (item.RowIndex), (item.ColIndex)));
                    }
                }
            }
            return resultadoReporte.ToString();
        }

        public void ConstruirListaTiposGrillaPrincipalColumnas()
        {
            //por ahora quemamos por codigo los nombres de las columnas y su ubicacion de solo la primera fila,
            //teniendo en cuenta las columnas fusionadas, ojo con estas ya que cuentan solo como 1 las internas quedan en null
            _listaTiposTablaLaicon = new Dictionary<int, NombreTiposColumnas>();

             _listaTiposTablaLaicon.Add(0, new NombreTiposColumnas(typeof(String), "NOMBRE VIA", true));
        }

        public ItemEstructura CrearItemTablaPrincipal(object valorCelda, int rowIndex, int colIndex)
        {
            ItemEstructura nuevoItem = new ItemEstructura();
            Type tipo = _listaTiposTablaLaicon[colIndex].Tipo;
            string nombre = _listaTiposTablaLaicon[colIndex].Nombre;
            bool obligatorio = _listaTiposTablaLaicon[colIndex].Obligatorio;
            bool errorParcer = true;
            bool errorObligatorio = false;

            if ((tipo.Name == "Double" || tipo.Name == "Decimal") && valorCelda != null)
            {
                valorCelda = valorCelda.ToString().Replace(".", ",");
            }

            if (valorCelda == null)
            {
                errorParcer = false;
                if (obligatorio)
                {
                    errorObligatorio = true;
                }
            }
            else
            {
                errorParcer = validar.PuedeParserGenerico(ref valorCelda, tipo);
            }

            nuevoItem.ErrorObligatorio = errorObligatorio;
            nuevoItem.Elemento = valorCelda;
            nuevoItem.ErrorParcer = errorParcer;
            nuevoItem.Tipo = tipo;
            nuevoItem.Nombre = nombre;
            nuevoItem.RowIndex = rowIndex+1;
            nuevoItem.ColIndex = colIndex+1;

            return nuevoItem;
        }
    }
}