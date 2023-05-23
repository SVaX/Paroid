using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : Controller
    {
        DiplomContext db = new DiplomContext();
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            return await db.Comments.ToListAsync();
        }

        [HttpGet("{AppCommentId}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentByAppId(int id)
        {
            var comments = await db.Comments.Where(x => x.IdApp == id).ToListAsync();

            if (comments == null)
            {
                return NotFound();
            }

            return comments;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddComment(Comment comment)
        {
            db.Comments.Add(comment);
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

        [HttpDelete("{idComment}")]
        public async Task<ActionResult<bool>> DeleteComment(int id)
        {
            var comment = await db.Comments.Where(x => x.CommentId == id).FirstOrDefaultAsync();
            db.Comments.Remove(comment);
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

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateComment(Comment comment)
        {
            db.Entry(comment).State = EntityState.Modified;

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
