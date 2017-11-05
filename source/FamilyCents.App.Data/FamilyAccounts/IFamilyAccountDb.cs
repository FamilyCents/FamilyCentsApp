using FamilyCents.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyCents.App.Data.FamilyAccounts
{
  public interface IFamilyAccountDb
  {
    Task<CustomerBalance> GetBalance(int accountId, int customerId);
  }
}
