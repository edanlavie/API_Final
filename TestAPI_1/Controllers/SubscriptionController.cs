using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using TestAPI_1.Models;

namespace TestAPI_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly TestAPI_1DBContext _context;

        public SubscriptionController(TestAPI_1DBContext context)
        {
            _context = context;
        }

        // GET: api/Subscription
        [HttpGet]
        public async Task<ActionResult<Response>> GetSubscriptions()
        {
            var response = new Response();

            var subscriptions = await _context.Subscriptions.ToListAsync();

            if (subscriptions.Count > 0)
            {
                response.statusCode = 200;
                response.statusDescription = "Woo! Subscriptions found.";
            }
            else
            {
                response.statusCode = 404;
                response.statusDescription = "No subscriptions found.";
            }
            response.subscriptions.AddRange(subscriptions);
            return response;
        }

        // GET: api/Subscription/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetSubscription(int id)
        {
            var response = new Response();

            var subscriptions = await _context.Subscriptions.FindAsync(id);

            if (subscriptions != null)
            {
                response.statusCode = 200;
                response.statusDescription = "Woo! Subscription " + id + " found.";
                response.subscriptions.Add(subscriptions);
            }
            else
            {
                response.statusCode = 404;
                response.statusDescription = "Subscription not found.";
            }
            return response;
        }


        // POST: api/Subscription
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Subscription>> PostSubscription(Subscription subscriptions)
        {
          if (_context.Subscriptions == null)
          {
              return Problem("Entity set 'TestAPI_1DBContext.Subscriptions'  is null.");
          }
            _context.Subscriptions.Add(subscriptions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubscription", new { id = subscriptions.SubscriptionId }, subscriptions);
        }

        // DELETE: api/Subscription/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteSubscription(int id)
        {
            var response = new Response();
            var subscriptions = await _context.Subscriptions.FindAsync(id);

            if (subscriptions == null)
            {
                response.statusCode = 404;
                response.statusDescription = "Subscription not deleted.";
                return NotFound(response);
            }

            _context.Subscriptions.Remove(subscriptions);
            await _context.SaveChangesAsync();
            response.statusCode = 200;
            response.statusDescription = "Woo! Deleted subscription.";

            return response;
        }

        private bool SubscriptionExists(int id)
        {
            return (_context.Subscriptions?.Any(e => e.SubscriptionId == id)).GetValueOrDefault();
        }
    }
}
