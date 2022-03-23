using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Screechr.Api.Authentication;
using Screechr.Api.Authentication.BasicAuthScheme;
using Screechr.Api.Utils;
using Screechr.Api.Validators;
using Screechr.Core.Data.Entities;
using Screechr.Core.Data.Repositories;
using Screechr.Core.Services;
using Screechr.Data.Services;

namespace Screechr.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
                .AddFluentValidation(fv => {
                    fv.DisableDataAnnotationsValidation = true;
                    fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                });

            services.AddAuthentication(options => options.DefaultScheme = AuthConstants.SchemeName)
                    .AddScheme<BasicAuthSchemeOptions, BasicAuthenticationHandler>(AuthConstants.SchemeName, options =>
                    {
                  
                    });
          

            ConfigureDependencies(services);
        }

        private void ConfigureDependencies(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepositiory>();
            services.AddScoped<IScreechRepository, ScreechRepository>();
            services.AddScoped<IScreechService,ScreechService >();
            services.AddScoped<IScreechFilterCriteriaValidator, ScreechFilterCriteriaValidator>();
            services.AddScoped<ICustomAuthenticationManager, CustomAuthenticationManager>();
            services.AddSingleton<IDbContext, DbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
    
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
