using Microsoft.AspNetCore.Razor.TagHelpers;
using FreelanceDB.Models;

namespace FreelanceDB.Abstractions.Repository
{
    public interface ITagRepository
    {
        Task<long> CreateTag(string tagName);
        Task<TagModel> GetTag(long tagId);
        Task<List<TagModel>> GetAllTags(long taskId);
        Task<long> AddTaskTag(long taskId, long tagId);
        Task<long> DeleteTaskTag(long taskId, long tagId);
    }
}
