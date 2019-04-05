using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoTransmilenio.CargarArchivos
{
    public class EstructuraColumnasExcel
    {
        List<List<ItemEstructura>> _tablaLaiconEstructura = new List<List<ItemEstructura>>();
        public List<List<ItemEstructura>> TablaLaiconEstructura
        {
            get { return _tablaLaiconEstructura; }
            set { _tablaLaiconEstructura = value; }
        }

        List<List<ItemEstructura>> _tablaCarroNECEstructura = new List<List<ItemEstructura>>();
        public List<List<ItemEstructura>> TablaCarroNECEstructura
        {
            get { return _tablaLaiconEstructura; }
            set { _tablaLaiconEstructura = value; }
        }
        public EstructuraColumnasExcel()
        {

        }
    }
}
