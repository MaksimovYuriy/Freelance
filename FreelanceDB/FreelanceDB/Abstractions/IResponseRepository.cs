using FreelanceDB.Database.Context;
using FreelanceDB.Models;

namespace FreelanceDB.Abstractions
{
    public interface IResponseRepository
    {
        Task<List<ResponseModel>> GetAllResponses(long taksId);
        Task<long> CreateResponse(long taskId, long userId);
        Task<long> DeleteResponse(long taskId, long userId);
        Task<long> ClearAllResponses(ResponseModel response);
    }
}
