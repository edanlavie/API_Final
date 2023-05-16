using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NuGet.Packaging;
using TestAPI_1.Models;

namespace TestAPI_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly TestAPI_1DBContext _context;

        public PostController(TestAPI_1DBContext context)
        {
            _context = context;
        }

        // GET: api/Post
        [HttpGet]
        public async Task<ActionResult<Response>> GetPosts()
        {
            var response = new Response();
            var posts = await _context.Posts.ToListAsync();

            if(posts.Count > 0)
            {
                response.statusCode = 200;
                response.statusDescription = "Woo! Posts found.";
            }
            else
            {
                response.statusCode = 404;
                response.statusDescription = "No posts found.";
            }
            response.posts.AddRange(posts);
            return response;
        }

        // GET: api/Post/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetPost(int id)
        {
            var response = new Response();
            var posts = await _context.Posts.FindAsync(id);

            if (posts != null)
            {
                response.statusCode = 200;
                response.statusDescription = "Woo! Post " + id + " found.";
                response.posts.Add(posts);
            }
            else
            {
                response.statusCode = 404;
                response.statusDescription = "Post not found.";
            }
            return response;
        }

        // POST: api/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {

          if (_context.Posts == null)
          {
              return Problem("Entity set 'TestAPI_1DBContext.Posts'  is null.");
          }
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.PostId }, post);
        }

        // DELETE: api/Post/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeletePost(int id)
        {
            var response = new Response();
            var posts = await _context.Posts.FindAsync(id);

            if (posts == null)
            {
                response.statusCode = 404;
                response.statusDescription = "Post not deleted.";
                return NotFound(response);
            }

            _context.Posts.Remove(posts);
            await _context.SaveChangesAsync();
            response.statusCode = 200;
            response.statusDescription = "Woo! Deleted post.";

            return response;
        }

        private bool PostExists(int id)
        {
            return (_context.Posts?.Any(e => e.PostId == id)).GetValueOrDefault();
        }
    }
}
