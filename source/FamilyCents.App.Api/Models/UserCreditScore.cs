using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCents.App.Api.Models
{
  public class UserCreditScore
  {
    [Required]
    public decimal? MinCreditLimit { get; set; }
    [Required]
    public decimal? MaxCreditLimit { get; set; }
  }
}
