using FreelanceDB.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FreelanceDB.Contracts.Requests
{
    public record FilterTasksRequest
    (
        string? Head,
        string[]? Tags
    );
}
