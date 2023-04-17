using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupAPIProject.Data.Entities
{
    public class RetailerEntity : UserEntity
    {
    
       public virtual List<LocationEntity> Locations {get;set;} = new List<LocationEntity>();
       public virtual List<InventoryItemEntity> InventoryItems {get;set;} = new List<InventoryItemEntity>();
       public virtual List<PurchaseOrderEntity> PurchaseOrders {get;set;} = new List<PurchaseOrderEntity>();
       public virtual List<SalesOrderEntity> SalesOrders {get;set;} = new List<SalesOrderEntity>();
       public virtual List<PurchaseOrderItemEntity> PurchaseOrderItems {get;set;} = new List<PurchaseOrderItemEntity>();
       public virtual List<SalesOrderItemEntity> SalesOrderItems {get;set;} = new List<SalesOrderItemEntity>();

    }
}