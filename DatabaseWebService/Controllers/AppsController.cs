using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DatabaseWebService.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AppsController : ControllerBase
    {
        DiplomContext db = new DiplomContext();
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> GetApps()
        {
            return await db.Applications.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Application>>> GetApp(int id)
        {
            var app = await db.Applications.FirstOrDefaultAsync(x => x.AppId == id);
            if (app == null)
            {
                return NotFound();
            }
            return new ObjectResult(app);
        }
    }
    
}
