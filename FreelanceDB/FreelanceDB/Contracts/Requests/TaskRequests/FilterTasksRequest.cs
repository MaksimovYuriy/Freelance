using FreelanceDB.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FreelanceDB.Contracts.Requests.TaskRequests
{
    public record FilterTasksRequest
    (
        string? Head,
        string[]? Tags
    );
}
