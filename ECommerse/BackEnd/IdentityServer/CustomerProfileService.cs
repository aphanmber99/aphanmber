using IdentityServer4.Services;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using BackEnd.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Globalization;
using IdentityModel;

namespace BackEnd.IdentityServer
{
    public class CustomerProfileService : IProfileService
    {
        private readonly ILogger<CustomerProfileService> _logger;
        private readonly UserManager<User> _userManager;

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject?.GetSubjectId();
            if (sub == null)
            {
                throw new Exception("No sub claim present");
            }

            var user = await _userManager.FindByIdAsync(sub);
            if (user == null)
            {
                _logger.LogWarning("No user found matching subject Id: {0}", sub);
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString(CultureInfo.InvariantCulture)),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(CultureInfo.InvariantCulture)),
                    new Claim(JwtClaimTypes.Name, user.Email),
                    new Claim(JwtClaimTypes.Email, user.Email),
                };

                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var userRole in userRoles)
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, userRole));
                }

                context.IssuedClaims.AddRange(claims);

            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject?.GetSubjectId();
            if (sub == null)
            {
                throw new Exception("No subject Id claim present");
            }

            var user = await _userManager.FindByIdAsync(sub);
            if (user == null)
            {
                _logger.LogWarning("No user found matching subject Id: {0}", sub);
            }

            context.IsActive = user != null;
        }
    }
}
