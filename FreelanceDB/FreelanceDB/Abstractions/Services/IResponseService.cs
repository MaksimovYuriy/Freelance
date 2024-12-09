using FreelanceDB.Models;

namespace FreelanceDB.Abstractions.Services
{
    public interface IResponseService
    {
        Task<long> CreateTaskResponse(long taskId, long userId);
        Task<List<ResponseModel>> GetMyResposes(long userId);
        Task<List<ResponseModel>> GetTaskResponses(long taskId);
    }
}
