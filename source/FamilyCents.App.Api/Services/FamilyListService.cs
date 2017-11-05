using FamilyCents.App.Api.Models;
using FamilyCents.App.CreditEngine;
using FamilyCents.App.CreditEngine.ScoreComponents;
using FamilyCents.App.Data;
using FamilyCents.App.Data.Apis;
using FamilyCents.App.Data.FamilyAccounts;
using FamilyCents.App.Data.Local;
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
    private readonly IFamilyDb _familyTaskDb;
    private readonly IFamilyAccountDb _accountDb;
    private readonly IPaymentsApi _paymentsApi;

    public FamilyListService(ICustomersApi customersApi, IAccountsApi accountsApi, ITransactionsApi transactionsApi, IFamilyDb familyTaskDb, IFamilyAccountDb accountDb, IPaymentsApi paymentsApi)
    {
      _customersApi = customersApi;
      _accountsApi = accountsApi;
      _transactionsApi = transactionsApi;
      _familyTaskDb = familyTaskDb;
      _accountDb = accountDb;
      _paymentsApi = paymentsApi;
    }

    public async Task<List<FamilyMember>> ListFamilyMembers(int accountId)
    {
      var fetchAccountDetails = _accountsApi.MakeRequestAsync(new AccountApiRequest { AccountId = accountId });
      var fetchAccountCustomers = _customersApi.MakeRequestAsync(new CustomerApiRequest { AccountId = accountId });
      var fetchAccountTransactions = _transactionsApi.MakeRequestAsync(new TransactionApiRequest { AccountId = accountId });
      var fetchCreditLimits = _familyTaskDb.GetAllFamilyMembersCredit(accountId);
      var fetchAllTasks = _familyTaskDb.GetAllTasks(accountId);
      var fetchAccountPayments = _paymentsApi.MakeRequestAsync(new PaymentApiRequest { AccountId = accountId });

      var accountCustomers = (await fetchAccountCustomers).First().Customers;
      var fetchBalances = Task.WhenAll(accountCustomers.Select(customer => _accountDb.GetBalance(accountId, customer.CustomerId)));

      var accountDetails = await fetchAccountDetails;
      var accountTransactions = (await fetchAccountTransactions).First().CustomerTransactions;
      var balances = await fetchBalances;
      var creditLimits = await fetchCreditLimits;
      var completedTasks = (await fetchAllTasks).Where(task => task.ApprovedBy != null && task.CompletedBy != null).ToList();
      var accountPayments = await fetchAccountPayments;
      var account = accountDetails.Single();

      var actualAccountPayments = accountPayments.Single().Payment.Select(payment => new CreditEngine.Payment(payment.ToDateTimeOffset(), payment.ToDateTimeOffset(), (double)payment.TotalMonthlyBalance, (double)payment.TotalBalanceRemaining, (double)payment.TotalBalancePaid)).ToList();

      var now = DateTimeOffset.UtcNow;

      var familyMembers =
        from customer in accountCustomers
        join customerTransactions in accountTransactions
        on customer.CustomerId equals customerTransactions.CustomerId
        join customerBalance in balances
        on customer.CustomerId equals customerBalance.CustomerId
        join creditLimit in creditLimits
        on customer.CustomerId equals creditLimit.CustomerId
        into customerCreditLimits
        join complatedTask in completedTasks
        on customer.CustomerId equals complatedTask.CompletedBy.Value
        into customerCompletedTasks
        let creditLimit = customerCreditLimits.FirstOrDefault()
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
        let isAdmin = account.PrimaryCustomerId == customer.CustomerId
        let creditLimitChange = new CreditLimitChange(
          (double)customerBalance.Balance,
          (double)(creditLimit?.Previous ?? 0M),
          (double)(creditLimit?.Current ?? 0M),
          creditLimit?.WhenChanged ?? now)
        let consumptionScore = new ConsumptionScore(
          creditLimitChange,
          (double)customerBalance.Balance)
        let largePurchaseSchore = new LargePurchaseScore(
          customerTransactions.Transactions
            .Select(t => new CreditEngine.Transaction(t.ToDateTimeOffset(), (double)t.Amount))
            .ToList(),
          (double)(creditLimit?.Current ?? 0M))
        let lifespanScore = new LifespanScore(
          customerTransactions.Transactions
            .OrderBy(t => t.ToDateTimeOffset())
            .First()
            .ToDateTimeOffset())
        let tasksByMonth = customerCompletedTasks
          .Where(t => t.WhenCompleted.Value.AddDays(90) > now)
          .ToLookup(t => t.WhenCompleted.Value.ToNextMonth())
        let virtualBills = customerTransactions.Transactions
          .Where(t => t.ToDateTimeOffset().AddDays(90) > now)
          .GroupBy(t => t.ToDateTimeOffset().ToNextMonth())
          .Select(month =>
          {
            var monthlyTransactions = month.ToList();
            var monthlyCompletedTasks = tasksByMonth[month.Key]?.ToList() ?? new List<FamilyTask>();

            var transactionsTotal = monthlyTransactions.Sum(x => x.Amount);
            var completedTotal = monthlyCompletedTasks.Sum(x => x.Value);

            return new CreditEngine.Payment(month.Key, month.Key, (double)transactionsTotal,(double)(transactionsTotal-completedTotal), (double)completedTotal) ;
          })
          .ToList()
        let paymentScore = new PaymentScore(isAdmin ? actualAccountPayments : virtualBills) // TODO
        let utilitzationScore = new UtilizationScore(
          (double)(creditLimit?.Current ?? 0M), 
          (double)customerBalance.Balance)
        let score = new CreditScore(consumptionScore, largePurchaseSchore, lifespanScore, paymentScore, utilitzationScore)
        select new FamilyMember
        {
          CustomerId = customer.CustomerId,
          IsPrimary = isAdmin,
          Name = $"{customer.FirstName} {customer.LastName}",
          RecentTransactions = transactions,
          VirtualBalance = customerBalance.Balance,
          VirtualCreditLimit = creditLimit?.Current,
          MaxCreditLimit = creditLimit?.Max,
          MinCreditLimit = creditLimit?.Min,
          VirtualCreditScore = (int)score.Score,
        };

      return familyMembers.ToList();
    }
  }
}
