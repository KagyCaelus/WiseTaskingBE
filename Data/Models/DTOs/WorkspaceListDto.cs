﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.DTOs;
public class WorkspaceListDto {
    public int WorkspaceId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
