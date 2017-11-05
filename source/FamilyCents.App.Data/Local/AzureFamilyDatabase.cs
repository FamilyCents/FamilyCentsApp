using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;
using FamilyCents.App.Data.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Linq;

namespace FamilyCents.App.Data.Local
{
  public sealed class AzureFamilyDatabase : IFamilyDb
  {
    private readonly DocumentClient _db;

    public AzureFamilyDatabase()
    {
      _db = new DocumentClient(
        new Uri("https://familycents.documents.azure.com:443/"), 
        "4cUGOpweNj7Fgyrm5Xu7FKVTDHjUkQ1Bln6N5pTz7zUDeGh6IOyqSrbg962pasqwmQMbPVPkmyHljkWHailjjA==");
    }

    public async Task<FamilyTask> UpdateTask(int accountId, Guid taskId, int? completedBy, int? approvedBy)
    {
      var task = await GetTask(accountId, taskId);

      var updatedTask = task.Clone();

      if (completedBy.HasValue)
      {
        updatedTask.CompletedBy = completedBy;
        updatedTask.WhenCompleted = DateTimeOffset.UtcNow;
      }

      if (approvedBy.HasValue)
      {
        updatedTask.ApprovedBy = approvedBy;
        updatedTask.WhenApproved = DateTimeOffset.UtcNow;
      }

      await _db.UpsertDocumentAsync(
        UriFactory.CreateDocumentCollectionUri("FamilyCents", "FamilyTasks"),
        updatedTask);

      return updatedTask;
    }

    public async Task<FamilyTask> CreateTask(int accountId, int creator, string description, decimal value)
    {
      var task = new FamilyTask
      {
        TaskId = Guid.NewGuid(),
        AccountId = accountId,
        Description = description,
        Value = value,
        CreatedBy = creator,
        WhenCreated = DateTimeOffset.UtcNow,
      };

      var result = await _db.CreateDocumentAsync(
        UriFactory.CreateDocumentCollectionUri("FamilyCents", "FamilyTasks"), 
        task);

      return task;
    }

    public Task<ImmutableList<FamilyTask>> GetAllTasks(int accountId)
    {
      var result = _db.CreateDocumentQuery<FamilyTask>(UriFactory.CreateDocumentCollectionUri("FamilyCents", "FamilyTasks"))
        .Where(d => d.AccountId == accountId)
        .ToImmutableList();

      return Task.FromResult(result);
    }

    public async Task<FamilyTask> GetTask(int accountId, Guid taskId)
    {
      var doc = await _db.ReadDocumentAsync<FamilyTask>(
        UriFactory.CreateDocumentUri("FamilyCents", "FamilyTasks", taskId.ToString()),
        new RequestOptions { PartitionKey = new PartitionKey(accountId) });

      return doc.Document;
    }

    public async Task<CustomerCreditLimit> UpdateCreditLimit(int accountId, int customerId, decimal creditLimit)
    {
      var currentLimit = await GetCreditLimit(accountId, customerId);

      var updatedLimit = currentLimit?.Clone() ?? throw new Exception("Credit not yet established");

      updatedLimit.Current = creditLimit;

      await _db.UpsertDocumentAsync(
        UriFactory.CreateDocumentCollectionUri("FamilyCents", "FamilyMembers"),
        updatedLimit);

      return updatedLimit;
    }

    public Task<ImmutableList<CustomerCreditLimit>> GetAllFamilyMembersCredit(int accountId)
    {
      var result = _db.CreateDocumentQuery<CustomerCreditLimit>(UriFactory.CreateDocumentCollectionUri("FamilyCents", "FamilyMembers"))
        .Where(d => d.AccountId == accountId)
        .ToImmutableList();

      return Task.FromResult(result);
    }

    public async Task<CustomerCreditLimit> UpdateCreditLimitRange(int accountId, int customerId, decimal min, decimal max)
    {
      var currentLimit = await GetCreditLimit(accountId, customerId);

      var updatedLimit = currentLimit?.Clone() ?? new CustomerCreditLimit
      {
        CustomerCreditLimitId = Guid.NewGuid(),
        AccountId = accountId,
        CustomerId = customerId,
        Current = min,
        Max = max,
        Min = min,
      };

      updatedLimit.Max = max;
      updatedLimit.Min = min;

      await _db.UpsertDocumentAsync(
        UriFactory.CreateDocumentCollectionUri("FamilyCents", "FamilyMembers"),
        updatedLimit);

      return updatedLimit;
    }

    public Task<CustomerCreditLimit> GetCreditLimit(int accountId, int customerId)
    {
      var result = _db.CreateDocumentQuery<CustomerCreditLimit>(UriFactory.CreateDocumentCollectionUri("FamilyCents", "FamilyMembers"))
        .Where(d => d.AccountId == accountId && d.CustomerId == customerId)
        .ToList()
        .FirstOrDefault();

      return Task.FromResult(result);
    }
  }
}
