using GradingSystem.Service.Scoring.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GradingSystem.Service.Scoring
{
    public class Startup
    {
        public const string DefaultPolicy = nameof(DefaultPolicy);
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o =>
            {
                o.AddPolicy(DefaultPolicy, builder => builder
                    .SetIsOriginAllowed(hostName => true)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    );

                o.DefaultPolicyName = DefaultPolicy;
            });

            services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = GradingSystemAuthScheme.GradingSystemToken;
                cfg.DefaultChallengeScheme = GradingSystemAuthScheme.GradingSystemToken;
            })
                .AddJwtBearer(GradingSystemAuthScheme.GradingSystemToken, GradingSystemAuthScheme.GradingSystemToken, cfg =>
                  {
                      cfg.RequireHttpsMetadata = false;
                      cfg.SaveToken = true;

                      cfg.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidIssuer = Configuration["Auth:GradingSystemToken:Issuer"],
                          ValidAudience = Configuration["Auth:GradingSystemToken:Audience"],
                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth:GradingSystemToken:Key"]))
                      };

                  });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(DefaultPolicy, policy =>
                {
                    policy.AddAuthenticationSchemes(
                        GradingSystemAuthScheme.GradingSystemToken);
                    policy.RequireAuthenticatedUser();
                });
            });
            Dependencies.Register(services, Configuration);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseRouting();
            app.UseCors(DefaultPolicy);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
