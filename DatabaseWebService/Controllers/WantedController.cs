using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WantedController : Controller
    {
        DiplomContext db = new DiplomContext();
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wanted>>> GetWanted()
        {
            return await db.Wanteds.ToListAsync();
        }

        [HttpGet("{WantedId}")]
        public async Task<ActionResult<IEnumerable<Wanted>>> GetWanted(int id)
        {
            var cart = await db.Wanteds.FirstOrDefaultAsync(x => x.WantedId == id);
            if (cart == null)
            {
                return NotFound();
            }
            return new ObjectResult(cart);
        }

        [HttpGet("{idUserWanted}")]
        public async Task<ActionResult<IEnumerable<Wanted>>> GetWantedByUserId(int userId)
        {
            var cart = await db.Wanteds.Where(x => x.IdUser == userId).ToListAsync();
            if (cart == null)
            {
                return NotFound();
            }
            return cart;
        }

        [HttpGet("{userIdWanted}")]
        public async Task<ActionResult<IEnumerable<Application>>> GetAppFromWantedForUserId(int userId)
        {
            var cart = await db.Wanteds.Where(x => x.IdUser == userId).ToListAsync();
            var apps = new List<Application>();
            foreach (var lib in cart)
            {
                foreach (var app in db.Applications)
                {
                    if (lib.IdApp == app.AppId)
                    {
                        apps.Add(app);
                    }
                }
            }
            if (cart == null)
            {
                return NotFound();
            }
            return apps;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddWanted(Wanted cart)
        {
            db.Wanteds.Add(cart);
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

        [HttpDelete("{idWanted}")]
        public async Task<ActionResult<bool>> DeleteWanted(int id)
        {
            var cart = await db.Wanteds.Where(x => x.WantedId == id).FirstOrDefaultAsync();
            db.Wanteds.Remove(cart);
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

        [HttpPut("{idWanted}")]
        public async Task<ActionResult<bool>> UpdateWanted(int id, Wanted library)
        {
            if (id != library.WantedId)
            {
                return BadRequest();
            }

            db.Entry(library).State = EntityState.Modified;

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
