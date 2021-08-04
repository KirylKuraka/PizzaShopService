using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObjects;

namespace Contracts
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDTO userForAuth);
        Task<string> CreateToken();
        string GenerateRefreshToken();
        string GenerateRefreshToken(int size);

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
