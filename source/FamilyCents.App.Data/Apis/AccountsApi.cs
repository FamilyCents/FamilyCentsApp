using FamilyCents.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;

namespace FamilyCents.App.Data.Apis
{
  public sealed class AccountsApi : CapitalOneApi<AccountApiRequest, ImmutableList<Account>>
  {
    public AccountsApi() : base("au-hackathon/accounts/")
    {
    }
  }
}
