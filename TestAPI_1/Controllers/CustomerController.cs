using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TestAPI_1.Models;

namespace TestAPI_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly TestAPI_1DBContext _context;

        public CustomerController(TestAPI_1DBContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<Response>> GetCustomers()
        {
            var response = new Response();

            var customers = await _context.Customers.ToListAsync();


            if (customers.Count > 0)
            {
                response.statusCode = 200;
                response.statusDescription = "Woo! Customers found.";
            }
            else
            {
                response.statusCode = 404;
                response.statusDescription = "No customers found.";
            }
            response.customers.AddRange(customers);
            return response;
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetCustomer(int id)
        {
            var response = new Response();

            var customers = await _context.Customers.FindAsync(id);
            
            if (customers != null)
            {
                response.statusCode = 200;
                response.statusDescription = "Woo! Customer " + id + " found.";
                response.customers.Add(customers);
            }
            else
            {
                response.statusCode = 404;
                response.statusDescription = "Customer not found.";
            }
            return response;
        }

        // POST: api/Customer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customers)
        {
          if (_context.Customers == null)
          {
              return Problem("Entity set 'TestAPI_1DBContext.Customers'  is null.");
          }
            _context.Customers.Add(customers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customers.CustomerId }, customers);
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteCustomer(int id)
        {
            var response = new Response();
            var customers = await _context.Customers.FindAsync(id);

            if (customers == null)
            {
                response.statusCode = 404;
                response.statusDescription = "Customer not deleted.";
                return NotFound(response);
            }

            _context.Customers.Remove(customers);
            await _context.SaveChangesAsync();
            response.statusCode = 200;
            response.statusDescription = "Woo! Deleted customer.";

            return response;
        }

        private bool CustomerExists(int id)
        {
            return (_context.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }
    }
}
