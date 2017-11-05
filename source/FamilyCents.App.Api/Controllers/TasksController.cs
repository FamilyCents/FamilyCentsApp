﻿using FamilyCents.App.Api.Models;
using FamilyCents.App.Data.Apis;
using FamilyCents.App.Data.Local;
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
    private readonly IFamilyDb _tasksDb;
    private readonly ICustomersApi _customersApi;

    public TasksController(IFamilyDb tasksDb, ICustomersApi customersApi)
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
    public async Task<IActionResult> TaskCompleted(int accountId, Guid taskId, [FromBody]FamilyTaskUpdate update)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }

      // Validate CompletedBy

      if (update.CompletedBy.HasValue)
      {
        var accountUsers = await _customersApi.MakeRequestAsync(
        new CustomerApiRequest { AccountId = accountId, CustomerId = update.CompletedBy });

        if (!accountUsers.Single().Customers.Any())
        {
          return BadRequest("CompletedBy User Not Found");
        }
      }

      // Validate ApprovedBy

      if (update.ApprovedBy.HasValue)
      {
        var accountUsers = await _customersApi.MakeRequestAsync(
        new CustomerApiRequest { AccountId = accountId, CustomerId = update.ApprovedBy });

        if (!accountUsers.Single().Customers.Any())
        {
          return BadRequest("ApprovedBy User Not Found");
        }
      }

      await _tasksDb.UpdateTask(accountId, taskId, update.CompletedBy, update.ApprovedBy);

      return Ok();
    }
  }
}
