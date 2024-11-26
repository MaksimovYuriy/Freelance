using FreelanceDB.Abstractions.Repository;
using FreelanceDB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoryTestController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;

        public RepositoryTestController(ITagRepository repository)
        {
            _tagRepository = repository;
        }

        [HttpPost("AddTag")]
        public async Task<IActionResult> CreateTag([FromQuery] string tagName)
        {
            long newTagId = await _tagRepository.CreateTag(tagName);
            return Ok(newTagId);
        }

        [HttpGet("GetTag")]
        public async Task<IActionResult> GetTag([FromQuery] long tagId)
        {
            TagModel model = await _tagRepository.GetTag(tagId);
            return Ok(model);
        }

        [HttpPost("AddTaskTag")]
        public async Task<IActionResult> AddTaskTag([FromQuery] long taskId, long tagId)
        {
            long taskTagId = await _tagRepository.AddTaskTag(taskId, tagId);
            return Ok(taskTagId);
        }

        [HttpDelete("DeleteTaskTag")]
        public async Task<IActionResult> DeleteTaskTag([FromQuery] long taskId, long tagId)
        {
            long taskTagId = await _tagRepository.DeleteTaskTag(taskId, tagId);
            return Ok(taskTagId);
        }

        [HttpGet("GetAllTags")]
        public async Task<IActionResult> GetAllTags(long taksId)
        {
            List<TagModel> models = await _tagRepository.GetAllTags(taksId);
            return Ok(models);
        }
    }
}
