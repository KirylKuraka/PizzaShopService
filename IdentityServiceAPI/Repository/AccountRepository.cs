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
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<PagedList<Account>> GetAccountsAsync(AccountParameters parameters, bool trackChanges)
        {
            var accounts = await FindAll(trackChanges)
                                .Search(parameters.SearchTerm)
                                .Sort(parameters.OrderBy)
                                .ToListAsync();

            return PagedList<Account>.ToPagedList(accounts, parameters.PageNumber, parameters.PageSize);
            //return accounts;
        }

        public async Task<Account> GetAccountAsync(Guid accountID, bool trackChanges) =>
            await FindByCondition(c => c.UserID.Equals(accountID), trackChanges)
                .SingleOrDefaultAsync();

        public void CreateAccount(Account account) => Create(account);

        public void UpdateAccount(Account account) => Update(account);

        public void DeleteAccount(Account account) => Delete(account);
    }
}
