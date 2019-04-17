using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyectotransmilenio.Models
{
    public class EquiposModelos
    {
    }

    public class EquiposLaicon
    {
        public int Id { get; set; }
        public string NombreVia { get; set; }
    
    }

    public class vistaEquiposLaicon
    {
        public List<SelectListItem> listaTiposEquipo { get; set; }
        public string Tipo { get; set; }
        public List<EquiposLaicon> listaEquipos { get; set; }
    }
}