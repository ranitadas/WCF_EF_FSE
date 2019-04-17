using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PurchaseOrderProcessingSystem.DAC;

namespace PurchaseOrderProcessingSystem.BC
{
    public class PurchaseOrderBC
    {
        PurchaseOrderDAC purchaseOrderDAC = new PurchaseOrderDAC();
        public bool processCreatePurchaseOrder(PurchaseProcessDetail purchaseProcessDetail)
        {
            var result=purchaseOrderDAC.createPurchaseOrder(purchaseProcessDetail);
            return result;
        }

        public Entity.PurchaseProcessDetail RetrievePurchaseOrder(string purchaseOrderNo)
        {
            var result = purchaseOrderDAC.RetrievePurchaseOrderDAC(purchaseOrderNo);
            return result;
        }

        public bool processUpdatePurchaseOrder(Entity.PurchaseProcessDetail purchaseProcessDetail)
        {
            var result = purchaseOrderDAC.processUpdatePurchaseOrderDAC(purchaseProcessDetail);
            return result;
        }

        public bool processDeletePurchaseOrder(string purchaseOrderNo)
        {
            var result = purchaseOrderDAC.processDeletePurchaseOrderDAC(purchaseOrderNo);
            return result;
        }
    }
}