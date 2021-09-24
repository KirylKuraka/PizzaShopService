using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IAccountRepository AccountRepository { get; }
        IProductRepository ProductRepository { get; }
        IProductTypeRepository ProductTypeRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IDeliveryMethodRepository DeliveryMethodRepository { get; }
        IOrderedProductRepository  OrderedProductRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderStatusRepository OrderStatusRepository { get; }
        IPaymentMethodRepository PaymentMethodRepository { get; }
        Task SaveAsync();
    }
}
