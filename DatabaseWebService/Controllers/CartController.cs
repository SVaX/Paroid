using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        DiplomContext db = new DiplomContext();
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            return await db.Carts.ToListAsync();
        }

        [HttpGet("{CartId}")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCart(int id)
        {
            var cart = await db.Carts.FirstOrDefaultAsync(x => x.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }
            return new ObjectResult(cart);
        }

        [HttpGet("{userCartId}")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCartsByUserId(int userId)
        {
            var cart = await db.Carts.Where(x => x.IdUser == userId).ToListAsync();
            if (cart == null)
            {
                return NotFound();
            }
            return cart;
        }

        [HttpGet("{IdUserCart}")]
        public async Task<ActionResult<IEnumerable<Application>>> GetAppFromCartForUserId(int userId)
        {
            var cart = await db.Carts.Where(x => x.IdUser == userId).ToListAsync();
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
        public async Task<ActionResult<bool>> AddCart(Cart cart)
        {
            db.Carts.Add(cart);
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

        [HttpDelete("{idCart}")]
        public async Task<ActionResult<bool>> DeleteCart(int id)
        {
            var cart = await db.Carts.Where(x => x.CartId == id).FirstOrDefaultAsync();
            db.Carts.Remove(cart);
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

        [HttpPut("{idCart}")]
        public async Task<ActionResult<bool>> UpdateCart(int id, Cart cart)
        {
            if (id != cart.CartId)
            {
                return BadRequest();
            }

            db.Entry(cart).State = EntityState.Modified;

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
