using Booking.DataAccess;
using Booking.Domain;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;

namespace Booking.API.JWT
{
    public class JwtHandler
    {
        private readonly BookingDbContext _context;
        private readonly AppSettings _appSettings;

        public JwtHandler(AppSettings appSettings, BookingDbContext context)
        {
            this._appSettings = appSettings;
            _context = context;
        }

        public JwtTokenResponse MakeToken(User user)
        {
            //throw new Exception("MAKE TOKEN HIT");
            Guid tokenGuid = Guid.NewGuid();

            string tokenId = tokenGuid.ToString();


          //  Console.WriteLine($"[DEBUG] user.Role is null: {user.Role == null}");
          //  Console.WriteLine($"[DEBUG] user.Role?.Name: {user.Role?.Name}");

            // Queried fresh from the DB (rather than relying on user.Role.UseCases
            // being eager-loaded by whichever caller passed in this User) so this
            // works the same whether MakeToken is called from Login or Refresh.
            var useCaseIds = _context.RoleUseCases
                .Where(ru => ru.RoleId == user.RoleId)
                .Select(ru => ru.UseCase.UseCaseId)
                .ToList();

            var claims = new List<Claim>
            {
                 new Claim(JwtRegisteredClaimNames.Iss, _appSettings.JwtSettings.Issuer, ClaimValueTypes.String),
                 new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                 new Claim("FirstName", user.FirstName),
                 new Claim("LastName", user.LastName),
                 new Claim("Email", user.Email),
                 new Claim("Id", user.Id.ToString()),
                 new Claim(ClaimTypes.Role, user.Role.Name),
                 new Claim("TokenId", tokenId),
                 new Claim("UseCaseIds", string.Join(",", useCaseIds)),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtSettings.SecretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _appSettings.JwtSettings.Issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: now.AddSeconds(_appSettings.JwtSettings.DurationSeconds),
                signingCredentials: credentials);

            var refreshToken = Guid.NewGuid().ToString();

            var jwtToken = new AuthToken
            {
                CreatedAt = now,
                ExpiresAt = now.AddSeconds(_appSettings.JwtSettings.DurationSeconds),
                TokenId = tokenId,
                UserId = user.Id,
            };

            var refreshTokenEntity = new AuthToken
            {
                TokenId = refreshToken,
                CreatedAt = now,
                ExpiresAt = now.AddHours(_appSettings.JwtSettings.RefreshTokenHours),
                UserId = user.Id,
                JwtToken = jwtToken
            };

            _context.AuthTokens.Add(jwtToken);
            _context.AuthTokens.Add(refreshTokenEntity);
            _context.SaveChanges();
            return new JwtTokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken
            };
        }
    }
}
