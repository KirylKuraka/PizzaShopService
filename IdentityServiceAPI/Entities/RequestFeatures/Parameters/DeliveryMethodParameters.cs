using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures.Parameters
{
    public class DeliveryMethodParameters : RequestParameters
    {
        public DeliveryMethodParameters()
        {
            OrderBy = "DeliveryMethodName";
            SearchTerm = "";
        }
    }
}
