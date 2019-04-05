using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExcelLibrary.SpreadSheet;
using System.Text;
using System.IO;

namespace ProyectoTransmilenio.CargarArchivos
{
    public class ValidadorInformacionCarroComprasNEC
    {
        private Workbook _miexcel;
        EstructuraColumnasExcel _estructura;
        Dictionary<int, NombreTiposColumnas> _listaTiposTablaCarroComprasNEC;
        HerramientasLectorExcel validar = new HerramientasLectorExcel();

        public EstructuraColumnasExcel Estructura
        {
            get { return _estructura; }
            set { _estructura = value; }
        }

        public ValidadorInformacionCarroComprasNEC(Workbook miexcel)
        {
            _miexcel = miexcel;
            _estructura = new EstructuraColumnasExcel();
            ConstruirListaTiposGrillaPrincipalColumnas();
        }

        public string CargarXLSCarroComprasNEC()
        {
            Worksheet mihoja = _miexcel.Worksheets[0];//suponemos solo 1 hoja
            CellCollection cells = mihoja.Cells;
            string celdaContrato = cells[0, 1].ToString().Trim();
            celdaContrato = celdaContrato.Replace("CONTRATO","");
            Int64 numeroContrato = Convert.ToInt64(celdaContrato);
            for (int rowIndex = mihoja.Cells.FirstRowIndex + 2; rowIndex <= mihoja.Cells.LastRowIndex; rowIndex++)
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
                    ItemEstructura nuevoItem = null;
                    nuevoItem = CrearItemTablaPrincipal(numeroContrato, rowIndex, 4);
                    rowTablaPrincipal.Add(nuevoItem);
                    _estructura.TablaLaiconEstructura.Add(rowTablaPrincipal);
                }

            }

            return GenerarReporteCarroComprasNEC();
        }

        public string GenerarReporteCarroComprasNEC()
        {
            StringBuilder resultadoReporte = new StringBuilder();

            if (_estructura.TablaLaiconEstructura.Count() == 0)
            {
                resultadoReporte.AppendLine("El validador de Información ha fallado, el formato de carrito de compras NEC no esta diligenciado");
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
            _listaTiposTablaCarroComprasNEC = new Dictionary<int, NombreTiposColumnas>();

            _listaTiposTablaCarroComprasNEC.Add(1, new NombreTiposColumnas(typeof(string), "LINE DESCRIPTION", true));
            _listaTiposTablaCarroComprasNEC.Add(2, new NombreTiposColumnas(typeof(decimal), "V/U sin IVA", true));
            _listaTiposTablaCarroComprasNEC.Add(3, new NombreTiposColumnas(typeof(int), "LINE", true));
            _listaTiposTablaCarroComprasNEC.Add(4, new NombreTiposColumnas(typeof(Int64), "CONTRATO", true));
        }

        public ItemEstructura CrearItemTablaPrincipal(object valorCelda, int rowIndex, int colIndex)
        {
            ItemEstructura nuevoItem = new ItemEstructura();
            Type tipo = _listaTiposTablaCarroComprasNEC[colIndex].Tipo;
            string nombre = _listaTiposTablaCarroComprasNEC[colIndex].Nombre;
            bool obligatorio = _listaTiposTablaCarroComprasNEC[colIndex].Obligatorio;
            bool errorParcer = true;
            bool errorObligatorio = false;

            //if ((tipo.Name == "Double" || tipo.Name == "Decimal") && valorCelda != null)
            //{
            //    valorCelda = valorCelda.ToString().Replace(".", ",");
            //}

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
            nuevoItem.RowIndex = rowIndex + 1;
            nuevoItem.ColIndex = colIndex + 1;

            return nuevoItem;
        }
    }
}

