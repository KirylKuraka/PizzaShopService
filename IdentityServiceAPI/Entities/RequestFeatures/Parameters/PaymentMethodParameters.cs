using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures.Parameters
{
    public class PaymentMethodParameters : RequestParameters
    {
        public PaymentMethodParameters()
        {
            OrderBy = "PaymentMethodName";
            SearchTerm = "";
        }
    }
}
