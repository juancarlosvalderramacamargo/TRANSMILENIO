using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExcelLibrary.SpreadSheet;
using System.Text;
using System.IO;

namespace ProyectoTransmilenio.CargarArchivos
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

             _listaTiposTablaLaicon.Add(0, new NombreTiposColumnas(typeof(Int64), "Laicon Id", true));
             _listaTiposTablaLaicon.Add(1, new NombreTiposColumnas(typeof(string), "Mic Code", false));
             _listaTiposTablaLaicon.Add(2, new NombreTiposColumnas(typeof(Int64), "Model Id (PLU)", true));
             _listaTiposTablaLaicon.Add(3, new NombreTiposColumnas(typeof(string), "Model Description", true));
             _listaTiposTablaLaicon.Add(4, new NombreTiposColumnas(typeof(string), "Maker", false));
             _listaTiposTablaLaicon.Add(5, new NombreTiposColumnas(typeof(string), "Reference", false));
             _listaTiposTablaLaicon.Add(6, new NombreTiposColumnas(typeof(string), "Sub-reference", false));
             _listaTiposTablaLaicon.Add(7, new NombreTiposColumnas(typeof(string), "Value Type", false));
             _listaTiposTablaLaicon.Add(8, new NombreTiposColumnas(typeof(string), "Is Asset?", false));
             _listaTiposTablaLaicon.Add(9, new NombreTiposColumnas(typeof(Int64), "Plu", false));
             _listaTiposTablaLaicon.Add(10, new NombreTiposColumnas(typeof(string), "Element Type", true));
             _listaTiposTablaLaicon.Add(11, new NombreTiposColumnas(typeof(string), "Element Category", true));
             _listaTiposTablaLaicon.Add(12, new NombreTiposColumnas(typeof(string), "Element Category Description", false));
             _listaTiposTablaLaicon.Add(13, new NombreTiposColumnas(typeof(string), "Location Code", true));
             _listaTiposTablaLaicon.Add(14, new NombreTiposColumnas(typeof(string), "Location Name", false));
             _listaTiposTablaLaicon.Add(15, new NombreTiposColumnas(typeof(string), "Geo. Location Code", false));
             _listaTiposTablaLaicon.Add(16, new NombreTiposColumnas(typeof(string), "Geo. Location Name", false));
             _listaTiposTablaLaicon.Add(17, new NombreTiposColumnas(typeof(string), "Serial", true));
             _listaTiposTablaLaicon.Add(18, new NombreTiposColumnas(typeof(int), "Purchase Price", false));
             _listaTiposTablaLaicon.Add(19, new NombreTiposColumnas(typeof(string), "State", true));
             _listaTiposTablaLaicon.Add(20, new NombreTiposColumnas(typeof(int), "Total Quantity", false));
             _listaTiposTablaLaicon.Add(21, new NombreTiposColumnas(typeof(int), "Total Reserved", false));
             _listaTiposTablaLaicon.Add(22, new NombreTiposColumnas(typeof(string), "Location Asigned Code", false));
             _listaTiposTablaLaicon.Add(23, new NombreTiposColumnas(typeof(string), "Internal Location Code", false));
             _listaTiposTablaLaicon.Add(24, new NombreTiposColumnas(typeof(string), "In Inventory?", false));
             _listaTiposTablaLaicon.Add(25, new NombreTiposColumnas(typeof(string), "Is Spare Part?", false));
             _listaTiposTablaLaicon.Add(26, new NombreTiposColumnas(typeof(string), "Is Deleted?", false));
             _listaTiposTablaLaicon.Add(27, new NombreTiposColumnas(typeof(string), "Is Movement?", false));
             _listaTiposTablaLaicon.Add(28, new NombreTiposColumnas(typeof(string), "Air Waybill Number", false));
             _listaTiposTablaLaicon.Add(29, new NombreTiposColumnas(typeof(string), "Invoice Number", false));
             _listaTiposTablaLaicon.Add(30, new NombreTiposColumnas(typeof(int), "Insurance Value", false));
             _listaTiposTablaLaicon.Add(31, new NombreTiposColumnas(typeof(string), "Asset Type", true));
             _listaTiposTablaLaicon.Add(32, new NombreTiposColumnas(typeof(string), "Asset Category", false));
             _listaTiposTablaLaicon.Add(33, new NombreTiposColumnas(typeof(string), "Investment Item", false));
             _listaTiposTablaLaicon.Add(34, new NombreTiposColumnas(typeof(int), "Asset Value (cop)", false));
             _listaTiposTablaLaicon.Add(35, new NombreTiposColumnas(typeof(int), "Asset Value (usd)", false));
             _listaTiposTablaLaicon.Add(36, new NombreTiposColumnas(typeof(int), "Asset Trm", false));
             _listaTiposTablaLaicon.Add(37, new NombreTiposColumnas(typeof(int), "Nationalization Value (cop)", false));
             _listaTiposTablaLaicon.Add(38, new NombreTiposColumnas(typeof(int), "Nationalization Value (usd)", false));
             _listaTiposTablaLaicon.Add(39, new NombreTiposColumnas(typeof(int), "Nationalization Trm", false));
             _listaTiposTablaLaicon.Add(40, new NombreTiposColumnas(typeof(Int64), "Purchase Order", true));
             _listaTiposTablaLaicon.Add(41, new NombreTiposColumnas(typeof(string), "Purchase Order Line", false));
             _listaTiposTablaLaicon.Add(42, new NombreTiposColumnas(typeof(string), "Fa Number (wip/temp)", false));
             _listaTiposTablaLaicon.Add(43, new NombreTiposColumnas(typeof(string), "Fa Number", false));
             _listaTiposTablaLaicon.Add(44, new NombreTiposColumnas(typeof(string), "Fa Sub-number", false));
             _listaTiposTablaLaicon.Add(45, new NombreTiposColumnas(typeof(Int64), "Warehouse Entry", false));
             _listaTiposTablaLaicon.Add(46, new NombreTiposColumnas(typeof(string), "Car", false));
             _listaTiposTablaLaicon.Add(47, new NombreTiposColumnas(typeof(string), "Date Capitalization", false));
             _listaTiposTablaLaicon.Add(48, new NombreTiposColumnas(typeof(string), "Piece, Box Or Stowage", false));
             _listaTiposTablaLaicon.Add(49, new NombreTiposColumnas(typeof(string), "Classification", false));
             _listaTiposTablaLaicon.Add(50, new NombreTiposColumnas(typeof(string), "Reenabled", false));
             _listaTiposTablaLaicon.Add(51, new NombreTiposColumnas(typeof(int), "Containing Element", false));
             _listaTiposTablaLaicon.Add(52, new NombreTiposColumnas(typeof(string), "Commentaries", false));
             _listaTiposTablaLaicon.Add(53, new NombreTiposColumnas(typeof(string), "Inside", false));
             _listaTiposTablaLaicon.Add(54, new NombreTiposColumnas(typeof(string), "Order", false));
             _listaTiposTablaLaicon.Add(55, new NombreTiposColumnas(typeof(string), "Location Order", false));
             _listaTiposTablaLaicon.Add(56, new NombreTiposColumnas(typeof(string), "Direction Order", false));
             _listaTiposTablaLaicon.Add(57, new NombreTiposColumnas(typeof(string), "State Order", false));
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