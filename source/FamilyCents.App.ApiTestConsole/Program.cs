using FamilyCents.App.Data;
using FamilyCents.App.Data.Apis;
using FamilyCents.App.Data.Local;
using FamilyCents.App.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
      using (var accountsApi = new AccountsApi())
      {
        var taskApi = new AzureFamilyDatabase();

        await taskApi.CreateTask(123200000, 123230000, "Mow The Lawn", 5.00M);

        var accountId = 152300000;

        var response = await customersApi.MakeRequestAsync(new CustomerApiRequest { AccountId = accountId });
        var account = await accountsApi.MakeRequestAsync(new AccountApiRequest { AccountId = accountId });

        var primary = account.Single().PrimaryCustomerId;

        var random = new Random();

        var now = DateTimeOffset.UtcNow;

        foreach (var customer in response.Single().Customers)
        {
          var accountTransactions = await transactionsApi.MakeRequestAsync(new TransactionApiRequest { CustomerId = customer.CustomerId });

          var transactions =
            accountTransactions
            .Single().CustomerTransactions
            .Single().Transactions;

          var lowerBounds = random.Next(25, 250);
          var upperBounds = lowerBounds + (lowerBounds * random.NextDouble()) + random.Next(0, 25);

         // await taskApi.UpdateCreditLimitRange(accountId, customer.CustomerId, (decimal)lowerBounds, (decimal)Math.Round(upperBounds, 0));

          var balance = 0M;

          foreach (var transaction in transactions.Where(t => t.ToDateTimeOffset().AddDays(30) > now))
          {
            balance += transaction.Amount;

            var taskDescriptions = new[] { "Random Task" };

            var tasks = new List<(decimal value, string description, DateTimeOffset when)>();

            await taskApi.CreateTask(
              accountId: accountId,
              creator: primary,
              description: $"Paid back for transaction for {transaction.MerchantName}",
              value: transaction.Amount,
              whenCompleted: transaction.ToDateTimeOffset().AddDays(random.Next(0, 10)),
              whenCreated: transaction.ToDateTimeOffset());

            await Task.Delay(TimeSpan.FromSeconds(0.5));
          }
        }

        Console.ReadLine();
      }
    }
  }
}
