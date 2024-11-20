using FreelanceDB.Abstractions.Repository;
using FreelanceDB.Database.Context;
using FreelanceDB.Database.Entities;
using FreelanceDB.Models;
using Microsoft.EntityFrameworkCore;

namespace FreelanceDB.Database.Repositories
{
    public class ResponseRepository : IResponseRepository
    {
        private readonly FreelancedbContext _context;

        public ResponseRepository(FreelancedbContext context)
        {
            _context = context;
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
            Response responseEntity = new Response();
            responseEntity.UserId = userId;
            responseEntity.TaskId = taskId;

            await _context.Responses.AddAsync(responseEntity);
            await _context.SaveChangesAsync();

            return userId;
        }

        public async Task<long> DeleteResponse(long taskId, long userId)
        {
            var status = await _context.Responses.Where(response => 
            response.TaskId == taskId && response.UserId == userId).ExecuteDeleteAsync();

            if(status == 0)
            {
                throw new Exception("Unknown response");
            }

            return userId;
        }

        public async Task<List<ResponseModel>> GetAllResponses(long taskId)
        {
            var responses = await _context.Responses.Where(response =>
            response.TaskId == taskId).ToListAsync();

            List<ResponseModel> models = new List<ResponseModel>();
            foreach(var response in responses)
            {
                ResponseModel model = new ResponseModel();
                model.TaskId = response.TaskId;
                model.UserId = response.UserId;

                models.Add(model);
            }

            return models;
        }
    }
}
