using FamilyCents.App.Data.Models;
using System.Collections.Immutable;

namespace FamilyCents.App.Data.Apis
{
  public interface ITransactionsApi : IApi<TransactionApiRequest, ImmutableList<AccountCustomerTransactions>>
  {
  }
}