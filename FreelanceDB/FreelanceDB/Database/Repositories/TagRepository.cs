using FreelanceDB.Database.Context;
using FreelanceDB.Database.Entities;
using FreelanceDB.Models;
using Microsoft.EntityFrameworkCore;
using FreelanceDB.Database.Repositories.Repository;

namespace FreelanceDB.Database.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly FreelancedbContext _context;
        private readonly ILogger<TagRepository> _logger;

        public TagRepository(FreelancedbContext context, ILogger<TagRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<long> AddTaskTag(long taskId, long tagId)
        {
            TaskTag? target = await _context.TaskTags
                .FirstOrDefaultAsync(p => p.TaskId == taskId && p.TagId == tagId);
            if(target != null)
            {
                _logger.LogError($"Trying to add an existing tag to this task {DateTime.Now.ToString()}");
                throw new Exception("This taskTag is already exists");
            }

            Entities.Task? targetTask = await _context.Tasks.FirstOrDefaultAsync(p => p.Id == taskId);
            if (targetTask == null)
            {
                _logger.LogError($"An attempt to add a tag to a non-existent task {DateTime.Now.ToString()}");
                throw new Exception("Unknown taskId");
            }

            Tag? targetTag = await _context.Tags.FirstOrDefaultAsync(p => p.Id == tagId);
            if (targetTag == null)
            {
                _logger.LogError($"An attempt to add a non-existent tag to an task {DateTime.Now.ToString()}");
                throw new Exception("Unknown tagId");
            }

            TaskTag entity = new TaskTag();

            entity.TaskId = taskId;
            entity.TagId = tagId;

            await _context.TaskTags.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.TaskId;
        }

        public async Task<long> CreateTag(string tagName)
        {
            Tag entity = new Tag();
            entity.Tag1 = tagName;

            await _context.Tags.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<long> DeleteTaskTag(long taskId, long tagId)
        {
            TaskTag? target = await _context.TaskTags
                .FirstOrDefaultAsync(p => p.TaskId == taskId && p.TagId == tagId);
            if (target == null)
            {
                _logger.LogError($"Attempt to delete a non-existent tag {DateTime.Now.ToString()}");
                throw new Exception("This taskTag does not exists");
            }

            await _context.TaskTags.Where(p => p.Id == target.Id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();

            return target.Id;
        }

        public async Task<List<TagModel>> GetAllTags(long taskId)
        {
            Entities.Task? task = await _context.Tasks.FirstOrDefaultAsync(p => p.Id == taskId);

            if (task == null)
            {
                _logger.LogWarning($"An attempt to get tags of a non-existent task {DateTime.Now.ToString()}");
                throw new Exception("Unknown task");
            }

            List<TagModel> result = new List<TagModel>();
            List<TaskTag> entities = await _context.TaskTags.Where(p => p.TaskId == taskId).ToListAsync();

            foreach (var entity in entities)
            {
                TagModel model = new TagModel();
                Tag? tag = await _context.Tags.FirstOrDefaultAsync(p => p.Id == entity.TagId);

                if (tag == null)
                {
                    _logger.LogError($"A non-existent tag was found for the task: {taskId}" +
                        $" {DateTime.Now.ToString()}");
                    throw new Exception("Unknown tag id");
                }

                model.Id = tag.Id;
                model.TagName = tag.Tag1;

                result.Add(model);
            }

            return result;
        }

        public async Task<TagModel> GetTag(long tagId)
        {
            Tag? targetTag = await _context.Tags.FirstOrDefaultAsync(p => p.Id == tagId);

            if(targetTag == null)
            {
                _logger.LogWarning($"Trying to find a non-existent tag in the database {DateTime.Now.ToString()}");
                throw new Exception("Unknown tagId");
            }

            TagModel model = new TagModel();
            model.Id = targetTag.Id;
            model.TagName = targetTag.Tag1;

            return model;
        }
    }
}
