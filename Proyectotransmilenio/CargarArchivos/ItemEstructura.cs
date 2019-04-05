using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoTransmilenio.CargarArchivos
{
    public class ItemEstructura
    {
        #region Atributos

        bool _errorParcer;
        object _elemento;
        Type _tipo;  
        bool _errorObligatorio;
        string _nombre;
        int _rowIndex;
        int _colIndex;
        #endregion

        #region Propiedades

        public bool ErrorParcer
        {
            get { return _errorParcer; }
            set { _errorParcer = value; }
        }

        public object Elemento
        {
            get { return _elemento; }
            set { _elemento = value; }
        }

        public Type Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }
        
        public bool ErrorObligatorio
        {
            get { return _errorObligatorio; }
            set { _errorObligatorio = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public int RowIndex
        {
            get { return _rowIndex; }
            set { _rowIndex = value; }
        }
        public int ColIndex
        {
            get { return _colIndex; }
            set { _colIndex = value; }
        }

        #endregion

        #region Constructor

        public ItemEstructura()
        {

        }

        #endregion

        #region Metodos
        #endregion

        #region ManejadoresEventos
        #endregion
    }
}