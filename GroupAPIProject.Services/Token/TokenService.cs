using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GroupAPIProject.Data;
using GroupAPIProject.Data.Entities;
using GroupAPIProject.Models.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;

namespace GroupAPIProject.Services.Token
{
    public class TokenService<T> : ITokenService<T> where T : UserEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public TokenService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        public async Task<TokenResponse> GetTokenAsync(TokenRequest model)
        {

            T userEntity = await GetValidUserAsync(model);
            if (userEntity is null)
            {

                return null;
            }



            return GenerateToken(userEntity);
        }
        private async Task<T> GetValidUserAsync(TokenRequest model)
        {


            T? userEntity = await _context.Users.OfType<T>().FirstOrDefaultAsync(user => user.Username.ToLower() == model.Username.ToLower());
            if (userEntity is null)
                return null;
            PasswordHasher<T> passwordHasher = new PasswordHasher<T>();
            PasswordVerificationResult verifyPasswordResult = passwordHasher.VerifyHashedPassword(userEntity, userEntity.Password, model.Password);
            if (verifyPasswordResult == PasswordVerificationResult.Failed)
                return null;

            return userEntity;
        }
        private TokenResponse GenerateToken(T entity)
        {
            Claim[] claims = GetClaims(entity);

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(14),
                SigningCredentials = credentials
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            TokenResponse tokenResponse = new TokenResponse
            {
                Token = tokenHandler.WriteToken(token),
                IssuedAt = token.ValidFrom,
                Expires = token.ValidTo
            };

            return tokenResponse;
        }
        private Claim[] GetClaims(T user)
        {


            string userType = user.GetType().Name;

            Claim[] claims = new Claim[]{ new Claim("Id", user.Id.ToString()),new Claim("Username", user.Username),new Claim("Role", userType) };

            return claims;
        }
    }
}