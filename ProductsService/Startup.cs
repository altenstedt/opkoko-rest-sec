using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ProductsService
{
    public class Startup
    {
        private readonly JwksStore jwksStore = new JwksStore("http://localhost:4001/jwks");

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IClaimsTransformation, ClaimsTransformation>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.RequireHttpsMetadata = false; // Only for testing purposes
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = "urn:omegapoint:opkoko",
                        ValidAudience = "urn:omegapoint:presentation",
                        IssuerSigningKeys = jwksStore.SecurityKeys
                    };
                });

            services.AddMvc(config => {
                var policy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
