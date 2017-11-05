using FamilyCents.App.Api.Models;
using FamilyCents.App.Data;
using FamilyCents.App.Data.Apis;
using FamilyCents.App.Data.FamilyAccounts;
using FamilyCents.App.Data.FamilyTasks;
using FamilyCents.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCents.App.Api.Services
{
  public class FamilyListService : IFamilyListService
  {

    private readonly ICustomersApi _customersApi;
    private readonly IAccountsApi _accountsApi;
    private readonly ITransactionsApi _transactionsApi;
    private readonly IFamilyTaskDb _familyTaskDb;
    private readonly IFamilyAccountDb _accountDb;

    public FamilyListService(ICustomersApi customersApi, IAccountsApi accountsApi, ITransactionsApi transactionsApi, IFamilyTaskDb familyTaskDb, IFamilyAccountDb accountDb)
    {
      _customersApi = customersApi;
      _accountsApi = accountsApi;
      _transactionsApi = transactionsApi;
      _familyTaskDb = familyTaskDb;
      _accountDb = accountDb;
    }

    public async Task<List<FamilyMember>> ListFamilyMembers(int accountId)
    {
      var fetchAccountDetails = _accountsApi.MakeRequestAsync(new AccountApiRequest { AccountId = accountId });
      var fetchAccountCustomers = _customersApi.MakeRequestAsync(new CustomerApiRequest { AccountId = accountId });
      var fetchAccountTransactions = _transactionsApi.MakeRequestAsync(new TransactionApiRequest { AccountId = accountId });

      var accountCustomers = (await fetchAccountCustomers).First().Customers;
      var fetchBalances = Task.WhenAll(accountCustomers.Select(customer => _accountDb.GetBalance(accountId, customer.CustomerId)));

      var accountDetails = await fetchAccountDetails;
      var accountTransactions = (await fetchAccountTransactions).First().CustomerTransactions;
      var balances = await fetchBalances;

      var account = accountDetails.Single();

      var rnd = new Random();

      var familyMembers =
        from customer in accountCustomers
        join customerTransactions in accountTransactions
        on customer.CustomerId equals customerTransactions.CustomerId
        join customerBalance in balances
        on customer.CustomerId equals customerBalance.CustomerId
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
          CustomerId = customer.CustomerId,
          IsPrimary = account.PrimaryCustomerId == customer.CustomerId,
          Name = $"{customer.FirstName} {customer.LastName}",
          RecentTransactions = transactions,
          VirtualBalance = customerBalance.Balance,
          VirtualCreditLimit = Convert.ToDecimal(creditLimit),
          VirtualCreditScore = creditScore,
        };

      return familyMembers.ToList();
    }
  }
}
