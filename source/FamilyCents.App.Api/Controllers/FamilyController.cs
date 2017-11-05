using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FamilyCents.App.Data.Apis;
using FamilyCents.App.Data.Models;
using FamilyCents.App.Api.Models;
using FamilyCents.App.Data;
using FamilyCents.App.Data.FamilyTasks;
using FamilyCents.App.Data.FamilyAccounts;
using FamilyCents.App.Api.Services;

namespace FamilyCents.App.Api.Controllers
{
  public class FamilyController : Controller
  {
    private readonly IFamilyListService _familyListService;

    public FamilyController(IFamilyListService familyListService)
    {
      _familyListService = familyListService;
    }

    [HttpGet]
    public async Task<IActionResult> Default(int accountId)
    {
      return Json(await _familyListService.ListFamilyMembers(accountId));
    }

    [ActionName("User")] // Because Controller.User is already a thing
    [HttpGet("/api/[controller]/{accountId:int}/[action]/{customerId:int}")]
    public async Task<IActionResult> GetUser(int accountId, int customerId)
    {
      var allFamilyMembers = await _familyListService.ListFamilyMembers(accountId);

      var thisFamilyMember = allFamilyMembers.FirstOrDefault(f => f.CustomerId == customerId);

      if (thisFamilyMember == null)
      {
        return NotFound();
      }

      return Json(thisFamilyMember);
    }
  }
}
