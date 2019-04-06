using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyectotransmilenio.Models
{
    public class GenerarDatos
    {
        public string NombreTabla { get; set; }

        public int NumeroRegistros { get; set; }

        public List<SelectListItem> ListaTablas { get; set; }
    }
}