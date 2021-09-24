using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures.Parameters
{
    public class OrderStatusParameters : RequestParameters
    {
        public OrderStatusParameters()
        {
            OrderBy = "OrderStatusName";
            SearchTerm = "";
        }
    }
}
