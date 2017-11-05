using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCents.App.Api.Models
{
  public class FamilyTaskComletion
  {
    [Required]
    public int CompletedBy { get; set; }
  }
}
