using FreelanceDB.Database.Context;
using FreelanceDB.Models;

namespace FreelanceDB.Database.Repositories.Repository
{
    public interface IResponseRepository
    {
        Task<List<ResponseModel>> GetAllResponsesByTask(long taksId);
        Task<List<ResponseModel>> GetAllResponsesByUser(long userId);
        Task<long> CreateResponse(long taskId, long userId);
        Task<long> DeleteResponse(long taskId, long userId);
        Task<long> ClearAllResponses(ResponseModel response);
    }
}
