using FamilyCents.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyCents.App.Data.Apis
{
  public sealed class PaymentsApi : IApi<PaymentApiRequest, PaymentApiResponse>
  {
    public Task<PaymentApiResponse> MakeRequest(PaymentApiRequest request)
    {
      throw new NotImplementedException();
    }
  }
}
