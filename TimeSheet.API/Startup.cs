﻿using TimeSheet.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeSheet.Helper;

namespace TimeSheet.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            NLog.LogManager.LoadConfiguration(string.Concat(System.IO.Directory.GetCurrentDirectory(), "/NLog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add Configure Extension and Bll class.
            services.ConfigureRepository(Configuration);
            services.ConfigureRedisCache(Configuration);
            services.ConfigureBll();
            services.ConfigureHttpContextAccessor();
            services.ConfigureLoggerService();
            services.ConfigureCors();
            services.ConfigureJwtAuthen(Configuration);
            services.ConfigureCookieAuthen(Configuration);
            services.AddAutoMapper();
            services.AddMvc(opt =>
            {
                opt.UseApiGlobalConfigRoutePrefix(new RouteAttribute("api"));
                opt.Filters.Add(typeof(ValidateModelStateAttribute));
            });
            services.ConfigureGraphQL();
            services.ConfigureSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.ConfigureMiddleware();
            app.UseSwaager();
            app.UseCors("CorsPolicy");
            app.UseGraphQL();
            app.UseMvc();
        }
    }
}
