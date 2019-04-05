using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinqToExcel.Attributes;

namespace Proyectotransmilenio.Models
{
    public class FilaLaicon
    {
        [ExcelColumn("Laicon Id")]
        public long LaiconId { get; set; }

        [ExcelColumn("Model Id")]
        public long ModelIdPLU { get; set; }

        [ExcelColumn("Model Description")]
        public string ModelDescription { get; set; }

        [ExcelColumn("Element Type")]
        public string ElementType { get; set; }

        [ExcelColumn("Location Code")]
        public string LocationCode { get; set; }

        [ExcelColumn("Serial")]
        public string SerialFabricante { get; set; }

        [ExcelColumn("State")]
        public string State { get; set; }

        [ExcelColumn("Purchase Order")]
        public string PurchaseOrder { get; set; }
    }

    public class FilaCarritoNEC
    {
        public string lineDescription { get; set; }
        public string valor { get; set; }
        public string line { get; set; }
        public string plu1 { get; set; }
        public string plu2 { get; set; }
        public string plu3 { get; set; }
        public string plu4 { get; set; }
        public string plu5 { get; set; }
        public string plu6 { get; set; }
        public string plu7 { get; set; }
        public string contrato { get; set; }

    }
}