using FreelanceDB.Abstractions.Repository;
using FreelanceDB.Abstractions.Services;
using FreelanceDB.Models;

namespace FreelanceDB.Services
{
    public class ResponseService : IResponseService
    {
        private readonly IResponseRepository _responseRepository;

        public ResponseService(IResponseRepository responseRepository)
        {
            _responseRepository = responseRepository;
        }
        public async Task<long> CreateTaskResponse(long taskId, long userId)
        {
            long responseId = await _responseRepository.CreateResponse(taskId, userId);
            return responseId;
        }

        public async Task<List<ResponseModel>> GetMyResposes(long userId)
        {
            var responses = await _responseRepository.GetAllResponsesByUser(userId);
            return responses;
        }

        public async Task<List<ResponseModel>> GetTaskResponses(long taskId)
        {
            var responses = await _responseRepository.GetAllResponsesByTask(taskId);
            return responses;
        }
    }
}
