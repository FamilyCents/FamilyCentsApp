﻿using FamilyCents.App.Data.Apis;
using FamilyCents.App.Data.FamilyTasks;
using FamilyCents.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyCents.App.Data.FamilyAccounts
{
  public sealed class FamilyAccountDb : IFamilyAccountDb
  {
    private readonly ITransactionsApi _transactionsApi;
    private readonly IFamilyTaskDb _familyTaskDb;

    public FamilyAccountDb(ITransactionsApi transactionsApi, IFamilyTaskDb familyTaskDb)
    {
      _transactionsApi = transactionsApi;
      _familyTaskDb = familyTaskDb;
    }

    public async Task<CustomerBalance> GetBalance(int accountId, int customerId)
    {
      var fetchTransactions = _transactionsApi.MakeRequestAsync(new TransactionApiRequest { AccountId = accountId, CustomerId = customerId });
      var fetchFamilyTasks = _familyTaskDb.GetAllTasks(accountId);

      var accountTransactions = await fetchTransactions;
      var familyTasks = await fetchFamilyTasks;

      var purchases = accountTransactions.Single().CustomerTransactions.Single().Transactions.Cast<IAffectsBalance>();
      var payments = familyTasks.Where(task => task.CompletedBy != null && task.CompletedBy == customerId).Cast<IAffectsBalance>();

      return new CustomerBalance
      {
        Balance = purchases.Concat(payments).Sum(transaction => transaction.Amount),
        CustomerId = customerId,
      };
    }
  }
}
