using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PurchaseOrderProcessingSystem.Entity;

namespace PurchaseOrderProcessingSystem.DAC
{
    public class PurchaseOrderDAC
    {
        int rowAffected = 0;
        public bool createPurchaseOrder(PurchaseProcessDetail purchaseProcessDetail)
        {

            SUPPLIER supplier = new SUPPLIER
            {
                SUPLNO = purchaseProcessDetail.Supplier.SupplierNo,
                SUPLNAME = purchaseProcessDetail.Supplier.SupplierName,
                SUPLADDR = purchaseProcessDetail.Supplier.SupplierAddress
            };

            PODETAIL poDetail = new PODETAIL
            {
                PONO = purchaseProcessDetail.PurchaseOrderDetail.PurchaseOrderNo,
                ITCODE = purchaseProcessDetail.PurchaseOrderDetail.ItemCode,
                QTY = purchaseProcessDetail.PurchaseOrderDetail.Quantity
            };

            POMASTER POMaster = new POMASTER
            {
                PODATE = purchaseProcessDetail.PurchaseOrderMaster.PurchaseOrderDate,
                PONO = purchaseProcessDetail.PurchaseOrderMaster.PurchaseOrderNo,
                SUPLNO = purchaseProcessDetail.PurchaseOrderMaster.SupplierNo
            };

            ITEM item = new ITEM
            {
                ITCODE = purchaseProcessDetail.Item.ItemCode,
                ITDESC = purchaseProcessDetail.Item.ItemDescription,
                ITRATE = purchaseProcessDetail.Item.ItemRate
            };

            using (var POContext = new Entities())
            {
                POContext.SUPPLIER.Add(supplier);
                POContext.ITEM.Add(item);
                POContext.PODETAIL.Add(poDetail);
                POContext.POMASTER.Add(POMaster);
                rowAffected = POContext.SaveChanges();
            }

            if (rowAffected > 0)
                return true;
            else
                return false;

        }

        public bool processDeletePurchaseOrderDAC(string purchaseOrderNo)
        {
            Entities entContext = new Entities();
            using (var purchaseDetail = new Entities())
            {
                // PPDetail.Item = new Item();
                var pomaster = (from pm in purchaseDetail.POMASTER
                                join p in purchaseDetail.PODETAIL
                            on pm.PONO equals p.PONO
                            where pm.PONO == purchaseOrderNo
                            select pm).FirstOrDefault();
                var podetail = (from p in purchaseDetail.PODETAIL
                                join pm in purchaseDetail.POMASTER
                            on p.PONO equals pm.PONO
                                where p.PONO == purchaseOrderNo
                                select p).FirstOrDefault();

                var itemdetail = (from t in purchaseDetail.ITEM
                                  join p in purchaseDetail.PODETAIL
                                  on t.ITCODE equals p.ITCODE
                                  where p.PONO == purchaseOrderNo
                                  select t).FirstOrDefault();

                var supplier = (from s in purchaseDetail.SUPPLIER
                                  join p in purchaseDetail.POMASTER
                                  on s.SUPLNO equals p.SUPLNO
                                  where p.PONO == purchaseOrderNo
                                  select s).FirstOrDefault();

               
                purchaseDetail.POMASTER.Remove(pomaster);
                purchaseDetail.ITEM.Remove(itemdetail);
                purchaseDetail.SUPPLIER.Remove(supplier);
                purchaseDetail.PODETAIL.Remove(podetail);



                rowAffected = purchaseDetail.SaveChanges();


            }
            if (rowAffected > 0)
                return true;
            else
                return false;
        }

        public bool processUpdatePurchaseOrderDAC(Entity.PurchaseProcessDetail purchaseProcessDetail)
        {
            var PPDetail = new Entity.PurchaseProcessDetail();

            using (var purchaseDetail = new Entities())
            {
                // PPDetail.Item = new Item();
                var Item = (from p in purchaseDetail.PODETAIL
                            join pm in purchaseDetail.POMASTER
                            on p.PONO equals pm.PONO
                            join t in purchaseDetail.ITEM
                            on p.ITCODE equals t.ITCODE
                            join s in purchaseDetail.SUPPLIER
                            on pm.SUPLNO equals s.SUPLNO
                            where p.PONO == purchaseProcessDetail.PurchaseOrderDetail.PurchaseOrderNo
                            select p).FirstOrDefault();

                MapforUpdateItems(Item, purchaseProcessDetail);

                rowAffected=purchaseDetail.SaveChanges();


            }
            if (rowAffected > 0)
                return true;
            else
                return false;

        }

        private void MapforUpdateItems(PODETAIL item, Entity.PurchaseProcessDetail purchaseProcessDetail)
        {
            if (purchaseProcessDetail.Item != null)
            {
                //if (!string.IsNullOrWhiteSpace(purchaseProcessDetail.Item.ItemCode))
                //{
                //    item.ITEM.ITCODE = purchaseProcessDetail.Item.ItemCode;
                //}
                if (!string.IsNullOrWhiteSpace(purchaseProcessDetail.Item.ItemDescription))
                {
                    item.ITEM.ITDESC = purchaseProcessDetail.Item.ItemDescription;
                }
                if (purchaseProcessDetail.Item.ItemRate!=null )
                {
                    item.ITEM.ITRATE = purchaseProcessDetail.Item.ItemRate;
                }
            }
            if (purchaseProcessDetail.PurchaseOrderDetail != null)
            {
                if (purchaseProcessDetail.PurchaseOrderDetail.Quantity!=null)
                {
                    item.QTY = purchaseProcessDetail.PurchaseOrderDetail.Quantity;
                }
                //if (!string.IsNullOrWhiteSpace(purchaseProcessDetail.PurchaseOrderDetail.ItemCode))
                //{
                //    item.ITCODE = purchaseProcessDetail.Item.ItemCode;
                //}
                //if (!string.IsNullOrWhiteSpace(purchaseProcessDetail.PurchaseOrderDetail.PurchaseOrderNo))
                //{
                //    item.PONO = purchaseProcessDetail.PurchaseOrderDetail.PurchaseOrderNo;
                //}

            }
            if(purchaseProcessDetail.PurchaseOrderMaster!=null)
            {
                //if (!string.IsNullOrWhiteSpace(purchaseProcessDetail.PurchaseOrderMaster.PurchaseOrderNo))
                //{
                //    item.POMASTER.PONO = purchaseProcessDetail.PurchaseOrderMaster.PurchaseOrderNo;
                //}
                //if (!string.IsNullOrWhiteSpace(purchaseProcessDetail.PurchaseOrderMaster.SupplierNo))
                //{
                //    item.POMASTER.SUPLNO = purchaseProcessDetail.PurchaseOrderMaster.SupplierNo;
                //}
                if (purchaseProcessDetail.PurchaseOrderMaster.PurchaseOrderDate!=null)
                {
                    item.POMASTER.PODATE = purchaseProcessDetail.PurchaseOrderMaster.PurchaseOrderDate;
                }
            }
            if (purchaseProcessDetail.Supplier != null)
            {
                //if (!string.IsNullOrWhiteSpace(purchaseProcessDetail.Supplier.SupplierNo))
                //{
                //    item.POMASTER.SUPPLIER.SUPLNO = purchaseProcessDetail.Supplier.SupplierNo;
                //}
                if (!string.IsNullOrWhiteSpace(purchaseProcessDetail.Supplier.SupplierName))
                {
                    item.POMASTER.SUPPLIER.SUPLNAME = purchaseProcessDetail.Supplier.SupplierName;
                }
                if (!string.IsNullOrWhiteSpace(purchaseProcessDetail.Supplier.SupplierAddress))
                {
                    item.POMASTER.SUPPLIER.SUPLADDR = purchaseProcessDetail.Supplier.SupplierAddress;
                }
            }
        }

        public Entity.PurchaseProcessDetail RetrievePurchaseOrderDAC(string purchaseOrderNo)
        {
            var PPDetail = new Entity.PurchaseProcessDetail();

            using (var purchaseDetail = new Entities())
            {
                // PPDetail.Item = new Item();
                var Item = (from p in purchaseDetail.PODETAIL
                            join pm in purchaseDetail.POMASTER
                            on p.PONO equals pm.PONO
                            join t in purchaseDetail.ITEM
                            on p.ITCODE equals t.ITCODE
                            join s in purchaseDetail.SUPPLIER
                            on pm.SUPLNO equals s.SUPLNO
                            where p.PONO == purchaseOrderNo
                            select p).FirstOrDefault();

                ConvertToPurchaseOrderEntity(Item, PPDetail);

            }


            return PPDetail;
        }


        private void ConvertToPurchaseOrderEntity(PODETAIL item, Entity.PurchaseProcessDetail ppDetail)
        {
            ppDetail.Item = new Entity.Item()
            {
                ItemCode = item.ITEM.ITCODE,
                ItemDescription = item.ITEM.ITDESC,
                ItemRate = item.ITEM.ITRATE
            };
            ppDetail.PurchaseOrderDetail = new Entity.PODetail()
            {
                Quantity = item.QTY
            };
            ppDetail.PurchaseOrderMaster = new Entity.POMaster()
            {
                PurchaseOrderDate = item.POMASTER.PODATE,
                PurchaseOrderNo = item.PONO
            };

            ppDetail.Supplier = new Entity.Supplier()
            {
                SupplierAddress = item.POMASTER.SUPPLIER.SUPLADDR,
                SupplierName = item.POMASTER.SUPPLIER.SUPLNAME,
                SupplierNo = item.POMASTER.SUPPLIER.SUPLNO
            };


        }
    }
}