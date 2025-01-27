﻿using FreelanceDB.Models;
using FreelanceDB.Database.Entities;
using FreelanceDB.Database.Context;
using Microsoft.EntityFrameworkCore;
using FreelanceDB.Database.Repositories.Repository;

namespace FreelanceDB.Database.Repositories
{
    public class ResumeRepository : IResumeRepository
    {
        private readonly FreelancedbContext _context;
        private readonly ILogger<ResumeRepository> _logger;

        public ResumeRepository(FreelancedbContext context, ILogger<ResumeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<long> CreateResume(ResumeModel resume, long userId)
        {
            Resume newResume = new Resume()
            {
                Head = resume.Head,
                WorkExp = resume.WorkExp,
                Skills = resume.Skills,
                Education = resume.Education,
                AboutMe = resume.AboutMe,
                Contacts = resume.Contacts,
                UserId = userId
            };

            await _context.Resumes.AddAsync(newResume);
            await _context.SaveChangesAsync();

          

            return resume.Id;
        }

        public async Task<long> DeleteResume(long id)
        {
            var status = await _context.Resumes.Where(resume => resume.Id == id).ExecuteDeleteAsync();

            if(status == 0)
            {
                _logger.LogWarning($"Attempt to delete a non-existent resume {DateTime.Now.ToString()}");
                throw new Exception("Unknown resume.id");
            }

            return id;
        }

        public async Task<List<ResumeModel>> GetAllResumes(long userId)
        {
            var resumes = await _context.Resumes.Where(p =>
            p.UserId == userId).ToListAsync();

            List<ResumeModel> models = new List<ResumeModel>();

            foreach(var resume in resumes)
            {
                ResumeModel resumeModel = new ResumeModel()
                {
                    Id = resume.Id,
                    Head = resume.Head,
                    WorkExp = resume.WorkExp,
                    Skills = resume.Skills,
                    Education = resume.Education,
                    AboutMe = resume.AboutMe,
                    Contacts = resume.Contacts
                };

                models.Add(resumeModel);
            }

            return models;
        }

        public async Task<ResumeModel> GetResume(long id)
        {
            var resumeEntity = await _context.Resumes.Where(resume => resume.Id == id).FirstOrDefaultAsync();

            if(resumeEntity == null)
            {
                _logger.LogWarning($"Attempt to get a non-existent resume {DateTime.Now.ToString()}");
                throw new Exception("Unknown resume.id");
            }

            ResumeModel resumeModel = new ResumeModel()
            {
                Id = resumeEntity.Id,
                Head = resumeEntity.Head,
                WorkExp = resumeEntity.WorkExp,
                Skills = resumeEntity.Skills,
                Education = resumeEntity.Education,
                AboutMe = resumeEntity.AboutMe,
                Contacts = resumeEntity.Contacts
            };

            return resumeModel;
        }

        public async Task<long> UpdateResume(long id, ResumeModel newResume)
        {
            await _context.Resumes.Where(resume => resume.Id == id)
                .ExecuteUpdateAsync(p =>
                p.SetProperty(m => m.Head, m => newResume.Head)
                .SetProperty(m => m.WorkExp, m => newResume.WorkExp)
                .SetProperty(m => m.Skills, m => newResume.Skills)
                .SetProperty(m => m.Education, m => newResume.Education)
                .SetProperty(m => m.AboutMe, m => newResume.AboutMe)
                .SetProperty(m => m.Contacts, m => newResume.Contacts));

            return id;
        }
    }
}
