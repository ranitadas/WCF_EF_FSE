using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PurchaseOrderProcessingSystem.Entity;

namespace PurchaseOrderProcessingSystem
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        
        [OperationContract]
        bool CreatePurchaseOrder(PurchaseProcessDetail value);

        [OperationContract]
        PurchaseProcessDetail RetrievePurchaseOrder(string purchaseOrderNo);

        [OperationContract]
        bool UpdatePurchaseOrder(PurchaseProcessDetail value);

        [OperationContract]
        bool DeletePurchaseOrder(string purchaseOrderNo);
       

        // TODO: Add your service operations here
    }

    [DataContract]
    public class PurchaseProcessDetail
    {
        [DataMember]
        public Item Item { get; set; }
        [DataMember]
        public PODetail PurchaseOrderDetail { get; set; }
        [DataMember]
        public POMaster PurchaseOrderMaster { get; set; }
        [DataMember]
        public Supplier Supplier { get; set; }
    }

    [DataContract]
    public class POMaster
    {
        [DataMember]
        public string PurchaseOrderNo { get; set; }
        [DataMember]
        public DateTime? PurchaseOrderDate { get; set; }
        [DataMember]
        public string SupplierNo { get; set; }
    }

    [DataContract]
    public class PODetail
    {
        [DataMember]
        public int? Quantity { get; set; }
        [DataMember]
        public string PurchaseOrderNo { get; set; }
        [DataMember]
        public string ItemCode { get; set; }
    }

    [DataContract]
    public class Supplier
    {
        [DataMember]
        public string SupplierNo { get; set; }
        [DataMember]
        public string SupplierName { get; set; }
        [DataMember]
        public string SupplierAddress { get; set; }
    }

    [DataContract]
    public class Item
    {
        [DataMember]
        public string ItemCode { get; set; }
        [DataMember]
        public string ItemDescription { get; set; }
        [DataMember]
        public decimal? ItemRate { get; set; }

        
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.

}
