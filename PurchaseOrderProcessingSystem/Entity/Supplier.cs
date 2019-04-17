using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PurchaseOrderProcessingSystem.Entity
{
    public class Supplier
    {
        
        public string SupplierNo { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
    }
}