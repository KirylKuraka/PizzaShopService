using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IAccountRepository _accountRepository;
        private IProductTypeRepository _productTypeRepository;
        private IProductRepository _productRepository;
        private ICustomerRepository _customerRepository;
        private IDeliveryMethodRepository _deliveryMethodRepository;
        private IOrderedProductRepository _orderedProductRepository;
        private IOrderRepository _orderRepository;
        private IOrderStatusRepository _orderStatusRepository;
        private IPaymentMethodRepository _paymentMethodRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IAccountRepository AccountRepository
        {
            get
            {
                if (_accountRepository == null)
                    _accountRepository = new AccountRepository(_repositoryContext);

                return _accountRepository;
            }
        }

        public IProductTypeRepository ProductTypeRepository
        {
            get
            {
                if (_productTypeRepository == null)
                    _productTypeRepository = new ProductTypeRepository(_repositoryContext);
                return _productTypeRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_repositoryContext);
                return _productRepository;
            }
        }

        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                    _customerRepository = new CustomerRepository(_repositoryContext);
                return _customerRepository;
            }
        }

        public IDeliveryMethodRepository DeliveryMethodRepository
        {
            get
            {
                if (_deliveryMethodRepository == null)
                    _deliveryMethodRepository = new DeliveryMethodRepository(_repositoryContext);
                return _deliveryMethodRepository;
            }
        }

        public IOrderedProductRepository OrderedProductRepository
        {
            get
            {
                if (_orderedProductRepository == null)
                    _orderedProductRepository = new OrderedProductRepository(_repositoryContext);
                return _orderedProductRepository;
            }
        }

        public IOrderRepository OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new OrderRepository(_repositoryContext);
                return _orderRepository;
            }
        }

        public IOrderStatusRepository OrderStatusRepository
        {
            get
            {
                if (_orderStatusRepository == null)
                    _orderStatusRepository = new OrderStatusRepository(_repositoryContext);
                return _orderStatusRepository;
            }
        }

        public IPaymentMethodRepository PaymentMethodRepository
        {
            get
            {
                if (_paymentMethodRepository == null)
                    _paymentMethodRepository = new PaymentMethodRepository(_repositoryContext);
                return _paymentMethodRepository;
            }
        }

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
