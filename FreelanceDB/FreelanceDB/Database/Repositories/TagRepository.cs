using FreelanceDB.Abstractions.Repository;
using FreelanceDB.Database.Context;
using FreelanceDB.Database.Entities;
using FreelanceDB.Models;
using Microsoft.EntityFrameworkCore;

namespace FreelanceDB.Database.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly FreelancedbContext _context;

        public TagRepository(FreelancedbContext context)
        {
            _context = context;
        }

        public async Task<long> AddTaskTag(long taskId, long tagId)
        {
            Entities.Task? targetTask = await _context.Tasks.FirstOrDefaultAsync(p => p.Id == taskId);
            if (targetTask == null)
            {
                throw new Exception("Unknown taskId");
            }

            Tag? targetTag = await _context.Tags.FirstOrDefaultAsync(p => p.Id == tagId);
            if (targetTag == null)
            {
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

        public async Task<TagModel> GetTag(long tagId)
        {
            Tag? targetTag = await _context.Tags.FirstOrDefaultAsync(p => p.Id == tagId);

            if(targetTag == null)
            {
                throw new Exception("Unknown tagId");
            }

            TagModel model = new TagModel();
            model.Id = targetTag.Id;
            model.TagName = targetTag.Tag1;

            return model;
        }
    }
}
