using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures.Parameters
{
    public class OrderParameters : RequestParameters
    {
        public OrderParameters()
        {
            OrderBy = "OrderDate";
            SearchTerm = "";
        }
    }
}
