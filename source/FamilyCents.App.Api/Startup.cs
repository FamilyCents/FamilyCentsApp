using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using FamilyCents.App.Data.Apis;
using FamilyCents.App.Data.Local;
using FamilyCents.App.Data.FamilyAccounts;
using FamilyCents.App.Api.Services;

namespace FamilyCents.App.Api
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
      services
        .AddSingleton<IAccountsApi, AccountsApi>()
        .AddSingleton<ITransactionsApi, TransactionsApi>()
        .AddSingleton<ICustomersApi, CustomersApi>()
        .AddSingleton<IFamilyDb, AzureFamilyDatabase>()
        .AddSingleton<IFamilyAccountDb, FamilyAccountDb>()
        .AddSingleton<IFamilyListService, FamilyListService>();

      services.AddCors();

      services.AddMvc();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

      app.UseMvc(options => {
        options.MapRoute(
          name: "default",
          template: "api/{controller}/{accountId:int}/{action=Default}");
      });
    }
  }
}
