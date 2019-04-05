using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TesGestionProyectos.CargarArchivos
{
    public class NombreTiposColumnas
    {
        Type _tipo;

        public Type Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        bool _obligatorio;

        public bool Obligatorio
        {
            get { return _obligatorio; }
            set { _obligatorio = value; }
        }

        public NombreTiposColumnas(Type tipo , string nombre, bool obligatorio)
        {
            _tipo = tipo;
            _nombre = nombre;
            _obligatorio = obligatorio;
        }
    }
}
