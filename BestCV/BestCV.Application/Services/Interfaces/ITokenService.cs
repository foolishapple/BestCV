using BestCV.Core.Entities;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(AccountToken account);
        bool ValidateToken(string authToken);
        JwtSecurityToken ParseToken(string tokenString);
        string GenerateToken(AccountToken account, DateTime expiryTime);
    }
}
