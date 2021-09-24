using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures.Parameters
{
    public class CustomerParameters : RequestParameters
    {
        public CustomerParameters()
        {
            OrderBy = "CustomerName";
            SearchTerm = "";
        }
    }
}
