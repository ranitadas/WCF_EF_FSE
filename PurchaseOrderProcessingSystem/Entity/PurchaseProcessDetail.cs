using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PurchaseOrderProcessingSystem.Entity
{
    public class PurchaseProcessDetail
    {
        public Item Item { get; set; }

        public PODetail PurchaseOrderDetail { get; set; }

        public POMaster PurchaseOrderMaster { get; set; }

        public Supplier Supplier { get; set; }
    }
}