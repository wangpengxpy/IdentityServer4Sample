using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace IDS4Demo.Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "oidc";
            })
           .AddCookie()
           .AddOpenIdConnect("oidc", options =>
           {
               options.AuthenticationMethod = OpenIdConnectRedirectBehavior.FormPost;
               options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
               options.Authority = "http://localhost:5000";
               options.RequireHttpsMetadata = false;

               options.ClientId = "dc6ce328-3693-46ba-b3c1-55b258e097a5";
               options.ClientSecret = "dZf1nxJQvb7M7IoNKDTkhTxFJTZF4J5mHE9z";
               options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
               options.SaveTokens = true;
               options.Scope.Clear();
               options.Scope.Add("openid");
               options.Scope.Add("profile");
               options.Scope.Add("order.admin");
               options.Scope.Add("offline_access");
               options.GetClaimsFromUserInfoEndpoint = true;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   NameClaimType = JwtClaimTypes.Name,
                   RoleClaimType = JwtClaimTypes.Role,
               };
               options.Events.OnTokenResponseReceived = (context) =>
               {
                   context.Response.Cookies.Append("ack", context.TokenEndpointResponse.AccessToken);
                   return Task.CompletedTask;
               };
           });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
