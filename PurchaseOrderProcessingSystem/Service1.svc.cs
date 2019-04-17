using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PurchaseOrderProcessingSystem.Entity;
using PurchaseOrderProcessingSystem.BC;

namespace PurchaseOrderProcessingSystem
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        PurchaseOrderBC purchaseOrderBC = new PurchaseOrderBC();
        public bool CreatePurchaseOrder(PurchaseProcessDetail purchaseProcessDetail)
        {
            var result = purchaseOrderBC.processCreatePurchaseOrder(purchaseProcessDetail);
            return result;
        }


        public PurchaseProcessDetail RetrievePurchaseOrder(string purchaseOrderNo)
        {
            var result = purchaseOrderBC.RetrievePurchaseOrder(purchaseOrderNo);
            PurchaseProcessDetail pPDetail = new PurchaseProcessDetail();
            MapEntityToPurchaseDetail(pPDetail, result);
            return pPDetail;
        }

        private void MapEntityToPurchaseDetail(PurchaseProcessDetail pPDetail, Entity.PurchaseProcessDetail result)
        {
            if (result != null)
            {
                if (result.Item != null)
                {
                    pPDetail.Item = new Item()
                    {
                        ItemCode = result.Item.ItemCode,
                        ItemDescription = result.Item.ItemDescription,
                        ItemRate = result.Item.ItemRate
                    };
                }
                if (result.PurchaseOrderDetail != null)
                {
                    pPDetail.PurchaseOrderDetail = new PODetail()
                    {
                        Quantity = result.PurchaseOrderDetail.Quantity
                    };
                }
                if (result.PurchaseOrderMaster != null)
                {
                    pPDetail.PurchaseOrderMaster = new POMaster()
                    {
                        PurchaseOrderDate = result.PurchaseOrderMaster.PurchaseOrderDate,
                        PurchaseOrderNo = result.PurchaseOrderMaster.PurchaseOrderNo
                    };
                }
                if (result.Supplier != null)
                {
                    pPDetail.Supplier = new Supplier()
                    {
                        SupplierAddress = result.Supplier.SupplierAddress,
                        SupplierName = result.Supplier.SupplierName,
                        SupplierNo = result.Supplier.SupplierNo
                    };
                }
            }
        }


        public bool UpdatePurchaseOrder(PurchaseProcessDetail purchaseProcessDetail)
        {
            var pPdetail = new Entity.PurchaseProcessDetail();
            MapPurchaseDetailToEntity(purchaseProcessDetail, pPdetail);
            var result = purchaseOrderBC.processUpdatePurchaseOrder(pPdetail);            
            return result;
        }

        private void MapPurchaseDetailToEntity(PurchaseProcessDetail result, Entity.PurchaseProcessDetail pPDetail)
        {
            if (result != null)
            {
                if (result.Item != null)
                {
                    pPDetail.Item = new Entity.Item()
                    {
                        ItemCode = result.Item.ItemCode,
                        ItemDescription = result.Item.ItemDescription,
                        ItemRate = result.Item.ItemRate
                    };
                }
                if (result.PurchaseOrderDetail != null)
                {
                    pPDetail.PurchaseOrderDetail = new Entity.PODetail()
                    {
                        PurchaseOrderNo=result.PurchaseOrderDetail.PurchaseOrderNo,
                        Quantity = result.PurchaseOrderDetail.Quantity,
                        ItemCode=result.Item.ItemCode
                    };
                }
                if (result.PurchaseOrderMaster != null)
                {
                    pPDetail.PurchaseOrderMaster = new Entity.POMaster()
                    {
                        PurchaseOrderDate = result.PurchaseOrderMaster.PurchaseOrderDate,
                        PurchaseOrderNo = result.PurchaseOrderMaster.PurchaseOrderNo
                    };
                }
                if (result.Supplier != null)
                {
                    pPDetail.Supplier = new Entity.Supplier()
                    {
                        SupplierAddress = result.Supplier.SupplierAddress,
                        SupplierName = result.Supplier.SupplierName,
                        SupplierNo = result.Supplier.SupplierNo
                    };
                }
            }
        }

        public bool DeletePurchaseOrder(string purchaseOrderNo)
        {
                       
            var result = purchaseOrderBC.processDeletePurchaseOrder(purchaseOrderNo);
            return result;

        }


    }
}
