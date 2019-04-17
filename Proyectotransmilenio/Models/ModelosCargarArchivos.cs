using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinqToExcel.Attributes;

namespace Proyectotransmilenio.Models
{
    public class FilaVia
    {
        [ExcelColumn("NOMBRE VIA")]
        public String nombreVia { get; set; }
    }
}