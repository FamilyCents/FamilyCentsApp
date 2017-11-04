using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FamilyCents.App.Data.Apis
{
  public abstract class CapitalOneApi<TRequest, TResponse> : IApi<TRequest, TResponse>, IDisposable
  {
    protected readonly HttpClient Client;
    protected readonly string ApiPath;
    private readonly JsonSerializerSettings _settings;

    public CapitalOneApi(string apiPath)
    {
      Client = new HttpClient()
      {
        BaseAddress = new Uri("https://3hkaob4gkc.execute-api.us-east-1.amazonaws.com/prod/")
      };
      ApiPath = apiPath;
      _settings = new JsonSerializerSettings
      {
        Formatting = Formatting.None,
        NullValueHandling = NullValueHandling.Ignore,
      };
    }

    void IDisposable.Dispose() => Client.Dispose();

    public virtual async Task<TResponse> MakeRequestAsync(TRequest request)
    {
      var json = JsonConvert.SerializeObject(request, _settings);

      using (var content = new StringContent(json, Encoding.Default, "application/json"))
      using (var res = await Client.PostAsync(ApiPath, content))
      {
        res.EnsureSuccessStatusCode();
        var jsonResponse = await res.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResponse>(jsonResponse);
      }
    }
  }
}
