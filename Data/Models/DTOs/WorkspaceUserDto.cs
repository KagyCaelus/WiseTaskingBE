using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.DTOs;
public class WorkspaceUserDto {
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Password { get; set; }
    [Required, Compare(nameof(Password))]
    public string RepeatPassword { get; set; }
    [Required]
    public int UserId { get; set; }
}
