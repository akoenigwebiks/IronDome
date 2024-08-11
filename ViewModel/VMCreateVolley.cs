using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IronDome.Models
{
    public class VMCreateVolley
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Launch Date")]
        public DateTime LaunchDate { get; set; }

        [Required]
        [Display(Name = "Launchers and Amounts")]
        public List<LauncherAmount> LauncherAmounts { get; set; } = new List<LauncherAmount>();
    }

    public class LauncherAmount
    {
        public int LauncherId { get; set; }
        public string LauncherName { get; set; } // For display purposes
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be at least 1")]
        public int Amount { get; set; }
    }
}
