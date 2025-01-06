using FreelanceDB.Database.Context;
using FreelanceDB.Database.Entities;
using FreelanceDB.Database.Repositories.Repository;
using FreelanceDB.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FreelanceDB.Database.Repositories
{
    public class ResponseRepository : IResponseRepository
    {
        private readonly FreelancedbContext _context;
        private readonly ILogger<ResponseRepository> _logger;

        public ResponseRepository(FreelancedbContext context, ILogger<ResponseRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Очистка всех ответов, если был выбран исполняющий для задачи
        public async Task<long> ClearAllResponses(ResponseModel response)
        {
            var status = await _context.Responses.Where(p =>
            p.TaskId == response.TaskId && p.UserId != response.UserId).ExecuteDeleteAsync();

            if(status == 0)
            {
                throw new Exception("Undefined");
            }

            return status;
        }

        public async Task<long> CreateResponse(long taskId, long userId)
        {
            Entities.Task? task = await _context.Tasks.FirstOrDefaultAsync(p => p.Id == taskId);
            User? user = await _context.Users.FirstOrDefaultAsync(p => p.Id == userId);

            if(task == null || user == null)
            {
                _logger.LogWarning($"An attempt to respond from {userId} to task {taskId} {DateTime.Now.ToString()}");
                throw new Exception("Unknown user or task");
            }

            Response responseEntity = new Response();
            responseEntity.UserId = userId;
            responseEntity.TaskId = taskId;

            responseEntity.ResponseDate = DateOnly.FromDateTime(DateTime.Now);

            await _context.Responses.AddAsync(responseEntity);
            await _context.SaveChangesAsync();

            return responseEntity.Id;
        }

        public async Task<long> DeleteResponse(long taskId, long userId)
        {
            var status = await _context.Responses.Where(response => 
            response.TaskId == taskId && response.UserId == userId).ExecuteDeleteAsync();

            if(status == 0)
            {
                _logger.LogWarning($"An attempt to delete a response from {userId} to task {taskId}" +
                    $" {DateTime.Now.ToString()}");
                throw new Exception("Unknown response");
            }

            return userId;
        }

        public async Task<List<ResponseModel>> GetAllResponsesByTask(long taskId)
        {
            var responses = await _context.Responses.Where(response =>
            response.TaskId == taskId).ToListAsync();

            List<ResponseModel> models = new List<ResponseModel>();
            foreach(var response in responses)
            {
                ResponseModel model = new ResponseModel();
                model.Id = response.Id;
                model.TaskId = response.TaskId;
                model.UserId = response.UserId;
                model.ResponseDate = response.ResponseDate;

                models.Add(model);
            }

            return models;
        }

        public async Task<List<ResponseModel>> GetAllResponsesByUser(long userId)
        {
            var responses = await _context.Responses.Where(response =>
            response.UserId == userId).ToListAsync();

            List<ResponseModel> models = new List<ResponseModel>();
            foreach (var response in responses)
            {
                ResponseModel model = new ResponseModel();
                model.Id = response.Id;
                model.TaskId = response.TaskId;
                model.UserId = response.UserId;
                model.ResponseDate = response.ResponseDate;

                models.Add(model);
            }

            return models;
        }
    }
}
