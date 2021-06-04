using System;
using ContosoPets.Ui.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

[assembly: HostingStartup(typeof(ContosoPets.Ui.Areas.Identity.IdentityHostingStartup))]
namespace ContosoPets.Ui.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                var connBuilder = new NpgsqlConnectionStringBuilder(
                    context.Configuration.GetConnectionString("ContosoPetsAuthConnection"))
                {
                    Username = context.Configuration["DbUsername"],
                    Password = context.Configuration["DbPassword"]
                };

                services.AddDbContext<ContosoPetsAuth>(options =>
                    options.UseNpgsql(connBuilder.ConnectionString));

                services.AddDefaultIdentity<ContosoPetsUser>()
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<ContosoPetsAuth>();
                });
        }
        
    }
}