using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;
using FamilyCents.App.Data.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Linq;

namespace FamilyCents.App.Data.FamilyTasks
{
  public sealed class CosmosDbTaskDb : IFamilyTaskDb
  {
    private readonly DocumentClient _db;

    public CosmosDbTaskDb()
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
  }
}
