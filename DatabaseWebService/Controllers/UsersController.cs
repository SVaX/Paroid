using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        DiplomContext db = new DiplomContext();
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await db.Users.ToListAsync();
        }

        [HttpGet("{UsersId}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(int id)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            return new ObjectResult(user);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddUser(User user)
        {
            db.Users.Add(user);
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

        [HttpDelete("{idUser}")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            var user = await db.Users.Where(x => x.UserId == id).FirstOrDefaultAsync();
            db.Users.Remove(user);
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

        [HttpPut("{idUser}")]
        public async Task<ActionResult<bool>> UpdateUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

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
