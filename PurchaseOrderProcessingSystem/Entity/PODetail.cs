using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PurchaseOrderProcessingSystem.Entity
{
    public class PODetail
    {
        public int? Quantity { get; set; }

        
        public string PurchaseOrderNo { get; set; }
      
        public string ItemCode { get; set; }
    }
}