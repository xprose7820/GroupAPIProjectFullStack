using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Data.Entities;
using GroupAPIProject.Models.Token;

namespace GroupAPIProject.Services.Token
{
    public interface ITokenService{
        Task<TokenResponse> GetTokenAsync<T>(TokenRequest model) where T: UserEntity;
    }
}