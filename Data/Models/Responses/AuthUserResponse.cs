using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Responses;
public class AuthUserResponse {
    public string AccessToken { get; set; }
    public int UserId { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
}
