using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FamilyCents.App.Data.Apis;
using FamilyCents.App.Data.Models;
using FamilyCents.App.Api.Models;
using FamilyCents.App.Data;

namespace FamilyCents.App.Api.Controllers
{
  public class FamilyController : Controller
  {
    private readonly ICustomersApi _customersApi;
    private readonly IAccountsApi _accountsApi;
    private readonly ITransactionsApi _transactionsApi;

    public FamilyController(ICustomersApi customersApi, IAccountsApi accountsApi, ITransactionsApi transactionsApi)
    {
      _customersApi = customersApi;
      _accountsApi = accountsApi;
      _transactionsApi = transactionsApi;
    }

    [HttpGet]
    public async Task<IActionResult> All(int accountId)
    {
      var fetchAccountDetails = _accountsApi.MakeRequestAsync(new AccountApiRequest { AccountId = accountId });
      var fetchAccountCustomers = _customersApi.MakeRequestAsync(new CustomerApiRequest { AccountId = accountId });
      var fetchAccountTransactions = _transactionsApi.MakeRequestAsync(new TransactionApiRequest { AccountId = accountId });

      var accountDetails = await fetchAccountDetails;
      var accountCustomers = await fetchAccountCustomers;
      var accountTransactions = await fetchAccountTransactions;

      var account = accountDetails.Single();
      var rnd = new Random();

      var familyMembers =
        from customer in accountCustomers.First().Customers
        join customerTransactions in accountTransactions.First().CustomerTransactions
        on customer.CustomerId equals customerTransactions.CustomerId
        let transactions = customerTransactions.Transactions
          .OrderByDescending(t => t.ToDateTimeOffset())
          .Take(5)
          .Select(t => new RecentTransaction
          {
            Merchant = t.MerchantName,
            Value = t.Amount,
            When = t.ToDateTimeOffset().Date.ToShortDateString()
          })
          .ToList()
        let creditLimit = rnd.Next(30, 100)
        let balance = rnd.Next(0, creditLimit)
        let creditScore = rnd.Next(300, 850)
        select new FamilyMember
        {
          IsPrimary = account.PrimaryCustomerId == customer.CustomerId,
          Name = $"{customer.FirstName} {customer.LastName}",
          RecentTransactions = transactions,
          VirtualBalance = Convert.ToDecimal(balance),
          VirtualCreditLimit = Convert.ToDecimal(creditLimit),
          VirtualCreditScore = creditScore,
        };

      return Json(familyMembers.ToList());
    }
  }
}
