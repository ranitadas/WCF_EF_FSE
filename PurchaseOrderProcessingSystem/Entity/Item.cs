using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PurchaseOrderProcessingSystem.Entity
{

    public class Item
    {
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public decimal? ItemRate { get; set; }
    }
}