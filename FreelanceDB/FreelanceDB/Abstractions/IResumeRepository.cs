using FreelanceDB.Database.Entities;
using FreelanceDB.Models;

namespace FreelanceDB.Abstractions
{
    public interface IResumeRepository
    {
        Task<ResumeModel> GetResume(long id);
        Task<long> CreateResume(ResumeModel resume);
        Task<long> DeleteResume(long id);
        Task<long> UpdateResume(long id, ResumeModel newResume);
    }
}
