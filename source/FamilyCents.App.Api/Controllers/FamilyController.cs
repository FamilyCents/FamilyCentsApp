using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FamilyCents.App.Api.Services;
using FamilyCents.App.Data.Local;
using FamilyCents.App.Api.Models;

namespace FamilyCents.App.Api.Controllers
{
  public class FamilyController : Controller
  {
    private readonly IFamilyListService _familyListService;
    private readonly IFamilyDb _faimilyDb;

    public FamilyController(IFamilyListService familyListService, IFamilyDb faimilyDb)
    {
      _familyListService = familyListService;
      _faimilyDb = faimilyDb;
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

    [ActionName("User")]
    [HttpPut("/api/[controller]/{accountId:int}/[action]/{customerId:int}")]
    public async Task<IActionResult> PutUser(int accountId, int customerId, [FromBody]UserCreditScore score)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }

      await _faimilyDb.UpdateCreditLimitRange(accountId, customerId, score.MinCreditLimit.Value, score.MaxCreditLimit.Value);

      return Ok();
    }
  }
}
