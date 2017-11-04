using FamilyCents.App.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FamilyCents.App.Data.Apis
{
  public sealed class CustomersApi : CapitalOneApi<CustomerApiRequest, ImmutableList<AccountCustomer>>
  {
    public CustomersApi() : base("au-hackathon/customers/")
    {
    }
  }
}
