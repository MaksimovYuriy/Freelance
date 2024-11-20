using FreelanceDB.Models;
using FreelanceDB.Database.Entities;
using FreelanceDB.Database.Context;
using Microsoft.EntityFrameworkCore;
using FreelanceDB.Abstractions.Repository;

namespace FreelanceDB.Database.Repositories
{
    public class ResumeRepository : IResumeRepository
    {
        private readonly FreelancedbContext _context;

        public ResumeRepository(FreelancedbContext context)
        {
            _context = context;
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
                Contacts = resume.Contacts
            };

            await _context.Resumes.AddAsync(newResume);
            await _context.SaveChangesAsync();

            UserResume userResume = new UserResume();
            userResume.UserId = userId;
            userResume.ResumeId = newResume.Id;

            return resume.Id;
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
