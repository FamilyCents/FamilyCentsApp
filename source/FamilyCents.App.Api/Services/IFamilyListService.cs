using System.Collections.Generic;
using System.Threading.Tasks;
using FamilyCents.App.Api.Models;

namespace FamilyCents.App.Api.Services
{
  public interface IFamilyListService
  {
    Task<List<FamilyMember>> ListFamilyMembers(int accountId);
  }
}