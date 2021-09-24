using Entities.Models;
using Entities.RequestFeatures;
using Entities.RequestFeatures.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPaymentMethodRepository
    {
        Task<PagedList<PaymentMethod>> GetPaymentMethodsAsync(PaymentMethodParameters parameters, bool trackChanges);
        Task<PaymentMethod> GetPaymentMethodAsync(Guid paymentMethodID, bool trackChanges);
        void CreatePaymentMethod(PaymentMethod paymentMethod);
        void UpdatePaymentMethod(PaymentMethod paymentMethod);
        void DeletePaymentMethod(PaymentMethod paymentMethod);
    }
}
