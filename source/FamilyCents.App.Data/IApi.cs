using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyCents.App.Data
{
  public interface IApi<TRequest,TResponse>
  {
    Task<TResponse> MakeRequestAsync(TRequest request);
  }
}
