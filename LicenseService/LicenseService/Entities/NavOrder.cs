using System;
using System.Collections.Generic;

namespace LicenseService.Entities
{
    public class NavOrder
    {
        public string OrderNumber;
        public int LineNumber;
        public IList<ProductOrder> ProductOrders;
        public int Action;
        public NavOrder()
        { ProductOrders = new List<ProductOrder>();}
    }
}
