using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PurchaseOrderProcessingSystem.Entity
{
    public class POMaster
    {
        public string PurchaseOrderNo { get; set; }
        public DateTime? PurchaseOrderDate { get; set; }
        public string SupplierNo { get; set; }
    }
}