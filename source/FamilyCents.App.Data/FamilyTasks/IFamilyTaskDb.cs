using FamilyCents.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;

namespace FamilyCents.App.Data.FamilyTasks
{
  public interface IFamilyTaskDb
  {
    Task<FamilyTask> CreateTask(int accountId, int creator, string description, decimal value);

    Task<FamilyTask> UpdateTask(int accountId, Guid taskId, int? completedBy, int? approvedBy);

    Task<FamilyTask> GetTask(int accountId, Guid taskId);

    Task<ImmutableList<FamilyTask>> GetAllTasks(int accountId);
  }
}
