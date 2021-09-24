using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures.Parameters
{
    public class ProductTypeParameters : RequestParameters
    {
        public ProductTypeParameters()
        {
            OrderBy = "ProductTypeName";
            SearchTerm = "";
        }
    }
}
