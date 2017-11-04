using FamilyCents.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;

namespace FamilyCents.App.Data.Apis
{
  public sealed class PaymentsApi : CapitalOneApi<PaymentApiRequest, ImmutableList<AccountPayment>>, IPaymentsApi
  {
    public PaymentsApi() : base("au-hackathon/payments/")
    {
    }
  }
}
