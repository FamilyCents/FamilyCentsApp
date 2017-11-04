using FamilyCents.App.Data;
using FamilyCents.App.Data.Apis;
using FamilyCents.App.Data.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
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
      using (var customersApi = new CustomersApi())
      using (var transactionsApi = new TransactionsApi())
      {
        var response = await customersApi.MakeRequestAsync(new CustomerApiRequest { AccountId = 123200000 });

        foreach (var customer in response.Single().Customers)
        {
          var accountTransactions = await transactionsApi.MakeRequestAsync(new TransactionApiRequest { CustomerId = customer.CustomerId });

          var transactions =
            accountTransactions
            .Single().CustomerTransactions
            .Single().Transactions;

          var averageTransaction = transactions.Average(x => x.Amount);
          var firstTransaction = transactions
            .OrderBy(t => t.ToDateTimeOffset())
            .First()
            .ToDateTimeOffset().Date
            .ToShortDateString();

          Console.WriteLine($"{customer.FirstName} {customer.LastName} made {transactions.Count} transaction since {firstTransaction} averaging {averageTransaction:C}");
        }

        Console.ReadLine();
      }
    }
  }
}
