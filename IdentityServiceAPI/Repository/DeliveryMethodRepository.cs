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
    public class DeliveryMethodRepository : RepositoryBase<DeliveryMethod>, IDeliveryMethodRepository
    {
        public DeliveryMethodRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<PagedList<DeliveryMethod>> GetDeliveryMethodsAsync(DeliveryMethodParameters parameters, bool trackChanges)
        {
            var deliveryMethods = await FindAll(trackChanges)
                                .Search(parameters.SearchTerm)
                                .Sort(parameters.OrderBy)
                                .ToListAsync();

            return PagedList<DeliveryMethod>.ToPagedList(deliveryMethods, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<DeliveryMethod> GetDeliveryMethodAsync(Guid deliveryMethodID, bool trackChanges) =>
            await FindByCondition(c => c.DeliveryMethodID.Equals(deliveryMethodID), trackChanges)
                .SingleOrDefaultAsync();

        public void CreateDeliveryMethod(DeliveryMethod deliveryMethod) => Create(deliveryMethod);

        public void UpdateDeliveryMethod(DeliveryMethod deliveryMethod) => Update(deliveryMethod);

        public void DeleteDeliveryMethod(DeliveryMethod deliveryMethod) => Delete(deliveryMethod);
    }
}
