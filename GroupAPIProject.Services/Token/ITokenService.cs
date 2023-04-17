using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Data.Entities;
using GroupAPIProject.Models.Token;

namespace GroupAPIProject.Services.Token
{
    public interface ITokenService<T> where T: UserEntity
    {
        Task<TokenResponse> GetTokenAsync(TokenRequest model);
    }
}