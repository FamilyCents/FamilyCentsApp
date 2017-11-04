using FamilyCents.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyCents.App.Data.Apis
{
  public sealed class CustomersApi : IApi<CustomerApiRequest, CustomerApiResponse>
  {
    public Task<CustomerApiResponse> MakeRequest(CustomerApiRequest request)
    {
      throw new NotImplementedException();
    }
  }
}
