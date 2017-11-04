using FamilyCents.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyCents.App.Data.Apis
{
  public sealed class TransactionsApi : IApi<TransactionApiRequest, TransactionApiResponse>
  {
    public Task<TransactionApiResponse> MakeRequest(TransactionApiRequest request)
    {
      throw new NotImplementedException();
    }
  }
}
