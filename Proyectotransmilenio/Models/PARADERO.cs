//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyectotransmilenio.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PARADERO
    {
        public decimal ID_PARADERO { get; set; }
        public decimal ID_TIPO_PAR { get; set; }
        public decimal ID_VIA { get; set; }
        public string NOMBRE_PARADERO { get; set; }
        public string UBICACION_PARADERO { get; set; }
        public decimal POSICION_VIA { get; set; }
    
        public virtual TIPOS_PARADEROS TIPOS_PARADEROS { get; set; }
        public virtual VIA VIA { get; set; }
    }
}
