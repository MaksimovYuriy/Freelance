﻿using FreelanceDB.Database.Context;
using FreelanceDB.Database.Entities;
using FreelanceDB.Models;
using Microsoft.EntityFrameworkCore;
using FreelanceDB.Database.Repositories.Repository;

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
            TaskTag? target = await _context.TaskTags
                .FirstOrDefaultAsync(p => p.TaskId == taskId && p.TagId == tagId);
            if(target != null)
            {
                throw new Exception("This taskTag is already exists");
            }

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

        public async Task<long> DeleteTaskTag(long taskId, long tagId)
        {
            TaskTag? target = await _context.TaskTags
                .FirstOrDefaultAsync(p => p.TaskId == taskId && p.TagId == tagId);
            if (target == null)
            {
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
                throw new Exception("Unknown tagId");
            }

            TagModel model = new TagModel();
            model.Id = targetTag.Id;
            model.TagName = targetTag.Tag1;

            return model;
        }
    }
}
