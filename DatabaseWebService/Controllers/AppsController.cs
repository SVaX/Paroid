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

        [HttpGet("{AppId}")]
        public async Task<ActionResult<IEnumerable<Application>>> GetApp(int id)
        {
            var app = await db.Applications.FirstOrDefaultAsync(x => x.AppId == id);
            if (app == null)
            {
                return NotFound();
            }
            return new ObjectResult(app);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddApp(Application app)
        {
            db.Applications.Add(app);
            try
            {
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete("{idApp}")]
        public async Task<ActionResult<bool>> DeleteApp(int id)
        {
            var app = await db.Applications.Where(x => x.AppId == id).FirstOrDefaultAsync();
            db.Applications.Remove(app);
            try
            {
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPut("{idApp}")]
        public async Task<ActionResult<bool>> UpdateApp(int id, Application application)
        {
            if (id != application.AppId)
            {
                return BadRequest();
            }

            db.Entry(application).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    
}
