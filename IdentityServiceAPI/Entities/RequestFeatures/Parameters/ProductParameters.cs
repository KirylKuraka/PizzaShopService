using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures.Parameters
{
    public class ProductParameters : RequestParameters
    {
        public ProductParameters()
        {
            OrderBy = "ProductName";
            SearchTerm = "";
        }
        public string FilterTerm { get; set; }
    }
}
