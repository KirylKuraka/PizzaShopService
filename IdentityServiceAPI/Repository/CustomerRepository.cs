using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.RequestFeatures.Parameters;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<PagedList<Customer>> GetCustomersAsync(CustomerParameters parameters, bool trackChanges)
        {
            var customers = await FindAll(trackChanges)
                                .Search(parameters.SearchTerm)
                                .Sort(parameters.OrderBy)
                                .ToListAsync();

            return PagedList<Customer>.ToPagedList(customers, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<Customer> GetCustomerAsync(Guid customerID, bool trackChanges) =>
            await FindByCondition(c => c.CustomerID.Equals(customerID), trackChanges)
                .SingleOrDefaultAsync();

        public void CreateCustomer(Customer customer) => Create(customer);

        public void UpdateCustomer(Customer customer) => Update(customer);

        public void DeleteCustomer(Customer customer) => Delete(customer);
    }
}
