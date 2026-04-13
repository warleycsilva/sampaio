using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Sampaio.Shared.Constants;
using Sampaio.Shared.Enums;
using Sampaio.Shared.ValueObjects;

namespace Sampaio.Shared.Security
{
    public class SessionUser
    {
        public Guid Id { get; set; }

        public string Email { get; set;}

        public string FullName => $"{FirstName} {LastName}".Trim();

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string AvatarUrl { get; set; }

        public EUserType? Type { get; set; }

        public bool? IsSysAdmin { get; set; }

        public bool? AcceptedTerms { get; set; }

        public Identification Identification { get; set; }
        
        public static SessionUser User(IEnumerable<Claim> claims)
        {
            var sessionUser = new SessionUser();
            
            var claimsArray = claims as Claim[] ?? claims.ToArray();
            
            sessionUser.Id = Guid.Parse(claimsArray.First(x => x.Type == CustomClaims.Id).Value);

            sessionUser.Email = claimsArray
                .FirstOrDefault(x => x.Type == CustomClaims.Email || x.Type == ClaimTypes.Email)?.Value;
          
            sessionUser.FirstName = claimsArray.FirstOrDefault(x => x.Type == CustomClaims.FirstName)
                ?.Value;
            
            sessionUser.LastName = claimsArray.FirstOrDefault(x => x.Type == CustomClaims.LastName)
                ?.Value;
            
            sessionUser.AvatarUrl = claimsArray.FirstOrDefault(x => x.Type == CustomClaims.AvatarUrl)
                ?.Value;

            sessionUser.AcceptedTerms =
                bool.Parse(claimsArray.FirstOrDefault(x => x.Type == CustomClaims.AcceptedTerms)?.Value ?? false.ToString());

            if (claimsArray.Any(x => x.Type == CustomClaims.Type && 
                                     !string.IsNullOrWhiteSpace(x.Value)) &&
                Enum.TryParse<EUserType>(claimsArray.First(x => x.Type == CustomClaims.Type).Value, out var type))
            {
                sessionUser.Type = type;
            }
            return sessionUser;
        }

        public ICollection<Claim> Claims()
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(CustomClaims.Id, Id.ToString()));
            claims.Add(new Claim(CustomClaims.Email, Email));
            claims.Add(new Claim(CustomClaims.FullName, FullName ?? ""));
            claims.Add(new Claim(CustomClaims.FirstName, FirstName ?? ""));
            claims.Add(new Claim(CustomClaims.LastName, LastName ?? ""));
            claims.Add(new Claim(CustomClaims.AvatarUrl, AvatarUrl ?? ""));
            

            if (Type.HasValue)
            {
                claims.Add(new Claim(CustomClaims.Type, Type.Value.ToString()));
                claims.Add(new Claim(CustomClaims.AcceptedTerms, AcceptedTerms.GetValueOrDefault(false).ToString()));
                claims.Add(new Claim(CustomClaims.Identification,Identification?.Formatted ?? ""));
            }
            return claims;
        }
        
        public virtual ClaimsPrincipal ClaimsPrincipal() => new ClaimsPrincipal(new[] {new ClaimsIdentity(Claims())});
    }
}
