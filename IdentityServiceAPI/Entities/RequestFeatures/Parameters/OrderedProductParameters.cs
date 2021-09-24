using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures.Parameters
{
    public class OrderedProductParameters : RequestParameters
    {
        public OrderedProductParameters()
        {
            OrderBy = "ProductName";
            SearchTerm = "";
        }
    }
}
