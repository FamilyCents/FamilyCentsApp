using FamilyCents.App.Api.Models;
using FamilyCents.App.Data.Apis;
using FamilyCents.App.Data.FamilyTasks;
using FamilyCents.App.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCents.App.Api.Controllers
{
  public class TasksController : Controller
  {
    private readonly IFamilyTaskDb _tasksDb;
    private readonly ICustomersApi _customersApi;

    public TasksController(IFamilyTaskDb tasksDb, ICustomersApi customersApi)
    {
      _tasksDb = tasksDb;
      _customersApi = customersApi;
    }

    [HttpGet]
    public async Task<IActionResult> Default(int accountId)
    {
      return Json(await _tasksDb.GetAllTasks(accountId));
    }

    [HttpGet("/api/[controller]/{accountId}/[action]/{taskId}")]
    public async Task<IActionResult> Task(int accountId, Guid taskId)
    {
      return Json(await _tasksDb.GetTask(accountId, taskId));
    }

    [HttpPost]
    public async Task<IActionResult> Task(int accountId, [FromBody]FamilyTaskCreation taskCreation)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }

      var accountUsers = await _customersApi.MakeRequestAsync(new CustomerApiRequest { AccountId = accountId, CustomerId = taskCreation.CreatedBy });

      if (!accountUsers.Single().Customers.Any())
      {
        return BadRequest("Customer Not Found");
      }

      var task = await _tasksDb.CreateTask(accountId, taskCreation.CreatedBy, taskCreation.Description, taskCreation.Value);

      return Created($"/Tasks/{accountId}/{nameof(Task)}/{task.TaskId}", new { task.TaskId });
    }

    [HttpPut("/api/[controller]/{accountId}/[action]/{taskId}")]
    [ActionName(nameof(Task))]
    public async Task<IActionResult> TaskCompleted(int accountId, Guid taskId, [FromBody]FamilyTaskComletion taskCompletion)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }

      var accountUsers = await _customersApi.MakeRequestAsync(new CustomerApiRequest { AccountId = accountId, CustomerId = taskCompletion.CompletedBy });

      if (!accountUsers.Single().Customers.Any())
      {
        return BadRequest("Customer Not Found");
      }

      await _tasksDb.CompleteTask(accountId, taskId, taskCompletion.CompletedBy);

      return Ok();
    }
  }
}
