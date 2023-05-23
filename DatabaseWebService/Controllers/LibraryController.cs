using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : Controller
    {
        DiplomContext db = new DiplomContext();
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Library>>> GetLibraries()
        {
            return await db.Libraries.ToListAsync();
        }

        [HttpGet("{LibraryId}")]
        public async Task<ActionResult<IEnumerable<Library>>> GetLibrary(int id)
        {
            var cart = await db.Libraries.FirstOrDefaultAsync(x => x.LibraryId == id);
            if (cart == null)
            {
                return NotFound();
            }
            return new ObjectResult(cart);
        }

        [HttpGet("{userIdLibrary}")]
        public async Task<ActionResult<IEnumerable<Library>>> GetLibraryByUserId(int userId)
        {
            var cart = await db.Libraries.Where(x => x.IdUser == userId).ToListAsync();
            if (cart == null)
            {
                return NotFound();
            }
            return cart;
        }

        [HttpGet("{idUserLibrary}")]
        public async Task<ActionResult<IEnumerable<Application>>> GetAppFromLibraryForUserId(int userId)
        {
            var cart = await db.Libraries.Where(x => x.IdUser == userId).ToListAsync();
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
        public async Task<ActionResult<bool>> AddLibrary(Library cart)
        {
            db.Libraries.Add(cart);
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

        [HttpDelete("{idLibrary}")]
        public async Task<ActionResult<bool>> DeleteLibrary(int id)
        {
            var cart = await db.Libraries.Where(x => x.LibraryId == id).FirstOrDefaultAsync();
            db.Libraries.Remove(cart);
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

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateCart(int id, Library library)
        {
            if (id != library.LibraryId)
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
