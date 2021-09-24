using Entities.Models;
using Entities.RequestFeatures;
using Entities.RequestFeatures.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IDeliveryMethodRepository
    {
        Task<PagedList<DeliveryMethod>> GetDeliveryMethodsAsync(DeliveryMethodParameters parameters, bool trackChanges);
        Task<DeliveryMethod> GetDeliveryMethodAsync(Guid deliveryMethodID, bool trackChanges);
        void CreateDeliveryMethod(DeliveryMethod deliveryMethod);
        void UpdateDeliveryMethod(DeliveryMethod deliveryMethod);
        void DeleteDeliveryMethod(DeliveryMethod deliveryMethod);
    }
}
