﻿using FreelanceDB.Abstractions;
using FreelanceDB.Models;
using FreelanceDB.Database.Entities;
using FreelanceDB.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace FreelanceDB.Database.Repositories
{
    public class ResumeRepository : IResumeRepository
    {
        private readonly FreelanceDbContext _context;

        public ResumeRepository(FreelanceDbContext context)
        {
            _context = context;
        }

        public async Task<long> CreateResume(ResumeModel resume)
        {
            Resume newResume = new Resume()
            {
                Head = resume.Head,
                WorkExp = resume.WorkExp,
                Skills = resume.Skills,
                Education = resume.Education,
                AboutMe = resume.AboutMe,
                Contacts = resume.Contacts
            };

            await _context.Resumes.AddAsync(newResume);
            await _context.SaveChangesAsync();

            return newResume.Id;
        }

        public async Task<long> DeleteResume(long id)
        {
            var status = await _context.Resumes.Where(resume => resume.Id == id).ExecuteDeleteAsync();

            if(status == 0)
            {
                throw new Exception("Unknown resume.id");
            }

            return id;
        }

        public async Task<ResumeModel> GetResume(long id)
        {
            var resumeEntity = await _context.Resumes.Where(resume => resume.Id == id).FirstOrDefaultAsync();

            if(resumeEntity == null)
            {
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