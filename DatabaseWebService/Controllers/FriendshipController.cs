using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipController : Controller
    {
        DiplomContext db = new DiplomContext();
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friendship>>> GetFriendships()
        {
            return await db.Friendships.ToListAsync();
        }

        [HttpGet("{FriendshipId}")]
        public async Task<ActionResult<IEnumerable<Friendship>>> GetFriendship(int id)
        {
            var cart = await db.Friendships.FirstOrDefaultAsync(x => x.FriendshipId == id);
            if (cart == null)
            {
                return NotFound();
            }
            return new ObjectResult(cart);
        }

        [HttpGet("{userIdFriendship}")]
        public async Task<ActionResult<IEnumerable<Friendship>>> GetNotConfirmedFriendshipByUserId(int userId)
        {
            var friendrequests = new List<Friendship>();
            foreach (var fr in db.Friendships)
            {
                if ((fr.IdUser1 == userId || fr.IdUser2 == userId) && !fr.Confirmed)
                {
                    friendrequests.Add(fr);
                }
            }
            return friendrequests;
        }

        [HttpGet("{idUserFriendship}")]
        public async Task<ActionResult<IEnumerable<Friendship>>> GetConfirmedFriendshipByUserId(int userId)
        {
            var friendships = new List<Friendship>();
            foreach (var fr in db.Friendships)
            {
                if ((fr.IdUser1 == userId || fr.IdUser2 == userId) && fr.Confirmed)
                {
                    friendships.Add(fr);
                }
            }
            return friendships;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddFriendship(Friendship friendship)
        {
            db.Friendships.Add(friendship);
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

        [HttpDelete("{idFriendship}")]
        public async Task<ActionResult<bool>> DeleteFriendship(int id)
        {
            var cart = await db.Friendships.Where(x => x.FriendshipId == id).FirstOrDefaultAsync();
            db.Friendships.Remove(cart);
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

        [HttpPut("{idFriendship}")]
        public async Task<ActionResult<bool>> UpdateFriendship(int id, Friendship library)
        {
            if (id != library.FriendshipId)
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
