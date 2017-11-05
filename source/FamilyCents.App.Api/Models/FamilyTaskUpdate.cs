using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCents.App.Api.Models
{
  public class FamilyTaskUpdate
  {
    public int? CompletedBy { get; set; }
    public int? ApprovedBy { get; set; }
  }
}
