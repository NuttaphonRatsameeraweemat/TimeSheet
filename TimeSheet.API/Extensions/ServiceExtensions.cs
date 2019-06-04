using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;

using TimeSheet.Helper.Interfaces;
using TimeSheet.Helper;
using TimeSheet.Bll.Interfaces;
using TimeSheet.Bll;
using TimeSheet.Data;
using TimeSheet.Data.Repository.Interfaces;
using TimeSheet.Bll.Models;
using GraphQL;
using TimeSheet.GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;

namespace TimeSheet.API.Extensions
{
    public static class ServiceExtensions
    {

        /// <summary>
        /// Dependency Injection Repository and UnitOfWork.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="Configuration">The configuration from settinfile.</param>
        public static void ConfigureRepository(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddEntityFrameworkSqlServer()
             .AddDbContext<TSContext>(options =>
              options.UseNpgsql(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddTransient<IUnitOfWork, TSUnitOfWork>();
        }

        /// <summary>
        /// Dependency Injection Class Business Logic Layer.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void ConfigureBll(this IServiceCollection services)
        {
            services.AddScoped<ILoginBll, LoginBll>();
            services.AddScoped<ITimeSheetBll, TimeSheetBll>();
            services.AddScoped<IValueHelpBll, ValueHelpBll>();
            services.AddScoped<IRegisterBll, RegisterBll>();
            services.AddScoped<IEmployeeBll, EmployeeBll>();
            services.AddScoped<IProjectBll, ProjectBll>();
            services.AddScoped<IRoleBll, RoleBll>();
            services.AddScoped<IDashBoardBll, DashBoardBll>();
            services.AddScoped<IManageToken, ManageToken>();
        }

        /// <summary>
        /// Dependency Injection Httpcontext.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void ConfigureHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        /// <summary>
        /// Add Singletion Logger Class
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        /// <summary>
        /// Config Api Routes Prefix.
        /// </summary>
        /// <param name="opts">The MvcOptions.</param>
        /// <param name="routeAttribute">The IRouteTemplateProvider.</param>
        public static void UseApiGlobalConfigRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute)
        {
            opts.Conventions.Insert(0, new ApiGlobalPrefixRouteProvider(routeAttribute));
        }

        /// <summary>
        /// Add Middleware when request begin and end.
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<Middleware>();
        }

        /// <summary>
        /// Add CORS Configuration.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        /// <summary>
        /// Configuration Swaager Doc and Authentication type.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "Header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);
            });
        }

        /// <summary>
        /// Setup Application Builder using Swagger and Swagger Ui.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

        /// <summary>
        /// Configuration GraphQL Dependency and Life time.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureGraphQL(this IServiceCollection services)
        {
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<AppSchema>();
            services.AddScoped<AppQuery>();
            services.AddScoped<EmployeeType>();

            services.AddGraphQL(o => { o.ExposeExceptions = false; })
                .AddGraphTypes(ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Setup Application Builder using GraphQL and GraphQL Ui Playground.
        /// </summary>
        /// <param name="app"></param>
        public static void UseGraphQL(this IApplicationBuilder app)
        {
            app.UseGraphQL<AppSchema>();
            app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());
        }

        /// <summary>
        /// Configuration Authentication Jwt type.
        /// </summary>
        /// <param name="services">The services conllection.</param>
        /// <param name="Configuration">The configuration.</param>
        public static void ConfigureJwtAuthen(this IServiceCollection services, IConfiguration Configuration)
        {
            var option = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = System.TimeSpan.Zero,
                ValidIssuer = Configuration["Jwt:Issuer"],
                ValidAudience = Configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            };
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = option;
                 options.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         context.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                         var model = new ResultViewModel
                         {
                             IsError = true,
                             Message = $"{context.Response.StatusCode}"
                         };
                         string json = JsonConvert.SerializeObject(model, new JsonSerializerSettings
                         {
                             ContractResolver = new CamelCasePropertyNamesContractResolver()
                         });
                         context.Response.OnStarting(async () =>
                         {
                             context.Response.ContentType = "application/json";
                             await context.Response.WriteAsync(json);
                         });
                         return System.Threading.Tasks.Task.CompletedTask;
                     },
                 };
             });
        }

        /// <summary>
        /// Configuration Services Cookie Authentication Jwt format.
        /// </summary>
        /// <param name="services">The services conllection.</param>
        /// <param name="Configuration">The configuration.</param>
        public static void ConfigureCookieAuthen(this IServiceCollection services, IConfiguration Configuration)
        {
            var option = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = System.TimeSpan.Zero,
                ValidIssuer = Configuration["Jwt:Issuer"],
                ValidAudience = Configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            };
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.Cookie.Name = "access_token";
                        options.SlidingExpiration = true;
                        options.Events.OnRedirectToLogin = context =>
                        {
                            context.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                            var model = new ResultViewModel
                            {
                                IsError = true,
                                Message = $"{context.Response.StatusCode}"
                            };
                            string json = JsonConvert.SerializeObject(model, new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                            });
                            context.Response.OnStarting(async () =>
                            {
                                context.Response.ContentType = "application/json";
                                await context.Response.WriteAsync(json);
                            });
                            return System.Threading.Tasks.Task.CompletedTask;
                        };
                        options.TicketDataFormat = new CookieAuthenticateFormat(SecurityAlgorithms.HmacSha256, option);
                    });
        }

    }
}
