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
    public class PaymentMethodRepository : RepositoryBase<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<PagedList<PaymentMethod>> GetPaymentMethodsAsync(PaymentMethodParameters parameters, bool trackChanges)
        {
            var paymentMethods = await FindAll(trackChanges)
                                .Search(parameters.SearchTerm)
                                .Sort(parameters.OrderBy)
                                .ToListAsync();

            return PagedList<PaymentMethod>.ToPagedList(paymentMethods, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PaymentMethod> GetPaymentMethodAsync(Guid paymentMethodID, bool trackChanges) =>
            await FindByCondition(c => c.PaymentMethodID.Equals(paymentMethodID), trackChanges)
                .SingleOrDefaultAsync();

        public void CreatePaymentMethod(PaymentMethod paymentMethod) => Create(paymentMethod);

        public void UpdatePaymentMethod(PaymentMethod paymentMethod) => Update(paymentMethod);

        public void DeletePaymentMethod(PaymentMethod paymentMethod) => Delete(paymentMethod);
    }
}
