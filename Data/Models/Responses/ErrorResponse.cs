using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Responses;
public class ErrorResponse {
    public IEnumerable<string> ErrorMessages { get; set; }

    public ErrorResponse(string errorResponse) : this(new List<string> { errorResponse })
    {
        
    }

    public ErrorResponse(IEnumerable<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}
