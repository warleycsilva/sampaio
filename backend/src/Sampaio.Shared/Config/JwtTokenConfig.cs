using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Sampaio.Shared.Config
{
    public class JwtTokenConfig
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecretKey { get; set; }

        public int ExpiresIn { get; set; }

        public SymmetricSecurityKey SigningKey =>
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public SigningCredentials SigningCredentials =>
            new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256);

        public TokenValidationParameters TokenValidationParameters => new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = Issuer,

            ValidateAudience = true,
            ValidAudience = Audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SigningKey,

            RequireExpirationTime = true,
            ValidateLifetime = true,

            ClockSkew = TimeSpan.Zero
        };
        
        public ClaimsPrincipal NewClaimsPrincipal(IEnumerable<Claim> claims)
        {
            var identity = new ClaimsIdentity(claims);
            return new ClaimsPrincipal(identity);
        }
        
        public string GenerateJwtToken(IEnumerable<Claim> claims, DateTime? expiration = null) => 
            GenerateJwtToken( NewClaimsPrincipal(claims), expiration);

        public string GenerateJwtToken(ClaimsPrincipal principal, DateTime? expiration = null)
        {
            expiration ??= DateTime.UtcNow.Add(TimeSpan.FromDays(ExpiresIn));
            
            var claims = new List<Claim>();
            claims.AddRange(principal.Claims);

            var jwt = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expiration,
                signingCredentials: SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var jwt = new JwtSecurityToken(token);
            if (jwt.ValidTo < DateTime.UtcNow)
                return null;
            var handler = new JwtSecurityTokenHandler();
            var principal = handler.ValidateToken(token, TokenValidationParameters, out var validToken);
            return principal;
        }

        public DateTime GetExpriration(string token)
        {
            var jwt = new JwtSecurityToken(token);
            return jwt.ValidTo;
        }

       
    }
}