using FamilyCents.App.Data.Apis;
using FamilyCents.App.Data.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace FamilyCents.App.ApiTestConsole
{ 
  /// <summary>
  /// Just a playground. Probably don't put anything important in here.
  /// </summary>
  class Program
  {
    static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

    static async Task MainAsync(string[] args)
    {
      using (var accountsApi = new CustomersApi())
      {
        var response = await accountsApi.MakeRequestAsync(new CustomerApiRequest
        {
          AccountId = 123200000
        });

        Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));

        Console.ReadLine();
      }
    }
  }
}
