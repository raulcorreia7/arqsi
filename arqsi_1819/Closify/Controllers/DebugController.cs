using System.Threading.Tasks;
using Closify.Data;
using Closify.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Closify.Controllers
{
    [Route("api/Debug")]
    [ApiController]
    public class DebugController : ControllerBase
    {

        private readonly ClosifyContext _context;
        private readonly IDebugRepository _repository;

        public DebugController(ClosifyContext context, IDebugRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet("Delete")]
        public IActionResult DeleteAll()
        {
            _repository.CleanDatabase();
            return Ok();
        }

        [HttpGet("Seed")]
        public IActionResult SeedDatabase()
        {
            Bootstrap.Initialize(_context);
            return Ok();
        }
    }
}
