using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCents.App.Api.Models
{
  public class FamilyTaskCreation
  {
    [Required]
    public string Description { get; set; }
    [Required]
    public int CreatedBy { get; set; }
    [Required]
    public decimal Value { get; set; }
  }
}
