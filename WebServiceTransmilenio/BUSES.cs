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
    
    public partial class BUSES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BUSES()
        {
            this.RUTAS = new HashSet<RUTAS>();
        }
    
        public decimal ID_BUS { get; set; }
        public decimal ID_TIPO_BUS { get; set; }
        public string MARCA { get; set; }
        public string MODELO { get; set; }
        public string CONDUTOR { get; set; }
        public string COLOR { get; set; }
    
        public virtual TIPOS_BUSES TIPOS_BUSES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RUTAS> RUTAS { get; set; }
    }
}
