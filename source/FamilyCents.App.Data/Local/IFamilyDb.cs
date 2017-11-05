using FamilyCents.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;

namespace FamilyCents.App.Data.Local
{
  public interface IFamilyDb
  {
    Task<FamilyTask> CreateTask(int accountId, int creator, string description, decimal value);

    Task<FamilyTask> UpdateTask(int accountId, Guid taskId, int? completedBy, int? approvedBy);

    Task<FamilyTask> GetTask(int accountId, Guid taskId);

    Task<ImmutableList<FamilyTask>> GetAllTasks(int accountId);

    Task<CustomerCreditLimit> UpdateCreditLimitRange(int accountId, int customerId, decimal min, decimal max);

    Task<CustomerCreditLimit> GetCreditLimit(int accountId, int customerId);

    Task<CustomerCreditLimit> UpdateCreditLimit(int accountId, int customerId, decimal creditLimit);

    Task<ImmutableList<CustomerCreditLimit>> GetAllFamilyMembersCredit(int accountId);
  }
}
