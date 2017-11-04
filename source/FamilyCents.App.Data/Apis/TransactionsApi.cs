using FamilyCents.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FamilyCents.App.Data.Apis
{
  public sealed class TransactionsApi : CapitalOneApi<TransactionApiRequest, ImmutableList<AccountCustomerTransactions>>, ITransactionsApi
  {
    public TransactionsApi() : base("au-hackathon/transactions/")
    {
    }
  }
}
