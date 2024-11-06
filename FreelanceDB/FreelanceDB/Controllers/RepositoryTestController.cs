using FreelanceDB.Abstractions;
using FreelanceDB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoryTestController : ControllerBase
    {
        private readonly IResumeRepository _repository;
        
        public RepositoryTestController(IResumeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetResumeByID(long id)
        {
            ResumeModel model = await _repository.GetResume(id);
            return Ok(model);
        }
    }
}
