using FreelanceDB.Database.Entities;
using FreelanceDB.Models;

namespace FreelanceDB.Database.Repositories.Repository
{
    public interface IResumeRepository
    {
        Task<ResumeModel> GetResume(long id);
        Task<long> CreateResume(ResumeModel resume, long userId);
        Task<long> DeleteResume(long id);
        Task<long> UpdateResume(long id, ResumeModel newResume);
        Task<List<ResumeModel>> GetAllResumes(long userId);
    }
}
