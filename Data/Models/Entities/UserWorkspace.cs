using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Entities;
public class UserWorkspace {
    public User User { get; set; }
    public int UserId { get; set; }
    public Workspace Workspace { get; set; }
    public int WorkspaceId { get; set; }
}
