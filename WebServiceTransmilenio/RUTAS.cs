//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebServiceTransmilenio
{
    using System;
    using System.Collections.Generic;
    
    public partial class RUTAS
    {
        public decimal ID_RUTA { get; set; }
        public decimal ID_BUS { get; set; }
        public decimal ID_TIPO_RUTA { get; set; }
        public string NOMBRE_RUTA { get; set; }
    
        public virtual BUSES BUSES { get; set; }
        public virtual TIPOS_RUTAS TIPOS_RUTAS { get; set; }
    }
}
