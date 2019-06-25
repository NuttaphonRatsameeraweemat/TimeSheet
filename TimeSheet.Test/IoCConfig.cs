using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeSheet.API;
using TimeSheet.Bll;
using TimeSheet.Bll.Interfaces;
using TimeSheet.Data;
using TimeSheet.Data.Repository.Interfaces;

namespace TimeSheet.Test
{
    /// <summary>
    /// The IoCConfig class provide installing all components needed to use.
    /// </summary>
    public class IoCConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IoCConfig" /> class.
        /// </summary>
        public IoCConfig()
        {
            // Load configuration file.
            var config = new ConfigurationBuilder()
                            .AddJsonFile(this.GetAppSettingDirectory())
                            .Build();

            var services = new ServiceCollection();
            // Add services to the container.
            services.AddEntityFrameworkSqlServer()
             .AddDbContext<TSContext>(options =>
              options.UseNpgsql(config["ConnectionStrings:DefaultConnection"]));
            
            services.AddSingleton<IConfiguration>(config);
            services.AddTransient<IUnitOfWork, TSUnitOfWork>();
            services.AddScoped<ILoginBll, LoginBll>();
            services.AddScoped<ITimeSheetBll, TimeSheetBll>();
            services.AddAutoMapper(typeof(Startup));

            ServiceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Get appsetting json file in unit test directory.
        /// </summary>
        /// <returns>appsetting directory path.</returns>
        private string GetAppSettingDirectory()
        {
            return string.Concat(System.IO.Directory.GetCurrentDirectory().Substring(0, System.IO.Directory.GetCurrentDirectory().IndexOf("bin")), "appsettings.json");
        }

        /// <summary>
        /// The Serivce Provider, this provides access to the IServiceCollection.
        /// </summary>
        public ServiceProvider ServiceProvider { get; private set; }

    }
}
