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
        public System.Guid Id { get; set; }
        public Int64 LaiconId { get; set; }
        public string ModelIdPLU { get; set; }
        public Int64 PO { get; set; }
        public string ModelDescription { get; set; }
        public string LocationCode { get; set; }
        public string Serial { get; set; }
        public string State { get; set; }
        public string AssetType { get; set; }          
        public Guid? IdEstacion { get; set; }
        public bool Seleccionado { get; set; }
    }

    public class vistaEquiposLaicon
    {
        public List<SelectListItem> listaTiposEquipo { get; set; }
        public string Tipo { get; set; }
        public List<EquiposLaicon> listaEquipos { get; set; }
    }
    public class CarritoNEC
    {
        public System.Guid Id { get; set; }
        public string Contrato { get; set; }
        public string Line { get; set; }
        public string ValorUnitarioSinIVA { get; set;}
        public string LineDescription { get; set; }
        public string plu { get; set; }
        public Guid? IdEstacion { get; set; }
        public bool Seleccionado { get; set; }
    }

    //public class EquiposReusar
    //{
    //    public List<TGProyectos_Sitios> Sitios { get; set; }
    //    public List<SelectListItem> SelectSitios { get; set; }
    //    public List<TGProyectos_EquiposReciclados> Equipos { get; set; }
    //    public List<SelectListItem> Proyectos { get; set; }
    //    public string IdProyecto { get; set; }

    //    public string IdSitio { get; set; }
    //}

    //public class AsignarASitioLaicon
    //{
    //    public List<TGProyectos_Sitios> Sitios { get; set; }

    //    public List<TGProyectos_EquiposLaicon> EquiposLaicon { get; set; }
    //    public List<SelectListItem> SelectSitios { get; set; }
    //    public List<SelectListItem> Proyectos { get; set; }
    //    public string IdProyecto { get; set; }

    //    public string IdSitio { get; set; }
    //}

    //public class AsignarASitioNEC
    //{
    //    public List<TGProyectos_Sitios> Sitios { get; set; }

    //    public List<TGProyectos_CarritoComprasNEC> EquiposNEC { get; set; }
    //    public List<SelectListItem> SelectSitios { get; set; }
    //    public List<SelectListItem> Proyectos { get; set; }
    //    public string IdProyecto { get; set; }

    //    public string IdSitio { get; set; }
    //}
}