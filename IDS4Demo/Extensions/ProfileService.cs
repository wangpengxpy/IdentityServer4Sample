// Copyright (c) Jeffcky <see cref="https://jeffcky.ke.qq.com/"/> All rights reserved.
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IDS4Demo
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<IdentityUser> _claimsFactory;
        private readonly UserManager<IdentityUser> _userManager;
        public ProfileService(UserManager<IdentityUser> userManager,
            IUserClaimsPrincipalFactory<IdentityUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = _userManager.GetUserAsync(context.Subject).Result;

            var principal = await _claimsFactory.CreateAsync(user);

            var claims = principal.Claims.ToList();

            //context.RequestedClaimTypes 本质上是指的具体某一个
            //ApiResources下面的UserClaims(即ApiClaims)和IdentityResources下面的IdentityClaims

            claims = claims.Where(d => context.RequestedClaimTypes.Contains(d.Type))
               .ToList();
            //context.IssuedClaims.AddRange(claims);

            if (!claims.Any(d => d.Type == JwtClaimTypes.Name))
            {
                claims.Add(new Claim(JwtClaimTypes.Name, user.UserName));
            }
            context.IssuedClaims.AddRange(claims);
        }

        Task IProfileService.IsActiveAsync(IsActiveContext context)
        {
            var user = _userManager.GetUserAsync(context.Subject).Result;
            context.IsActive = user != null;
            return Task.CompletedTask;
        }
    }
}
