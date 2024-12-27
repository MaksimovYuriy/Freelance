using FreelanceDB.Database.Entities;
using FreelanceDB.Models;

namespace FreelanceDB.Contracts.Responses.RespResponses
{
    public record RespsResponse
    (
        List<ResponseModel> responses
    );
}
