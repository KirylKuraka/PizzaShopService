using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IAccountRepository AccountRepository { get; }
        Task SaveAsync();
    }
}
