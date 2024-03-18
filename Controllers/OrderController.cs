using BackEndRestaurant.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEndRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private RestaurantContext dbContext = new RestaurantContext();


        [HttpGet]

        public IActionResult GetAll(string? restaurant, bool? another)
        {

            if (restaurant != null && another == null)
            {
                List<Order> result = dbContext.Orders.Where(x => x.Restaurant.ToLower() == restaurant.ToLower()).ToList();
                return Ok(result);
            }

            else if (restaurant == null && another != null)
            {
                List<Order> result = dbContext.Orders.Where(x => x.OrderAgain == another).ToList();
                return Ok(result);
            }

            else if (restaurant != null && another != null)

            {
                List<Order> result = dbContext.Orders.Where(x => x.Restaurant == restaurant && x.OrderAgain == another).ToList();
                return Ok(result);
            }
            else
            {
                List<Order> result = dbContext.Orders.ToList();
                return Ok(result);
            }
        }

        [HttpGet("{id}")]

        public IActionResult Getid(int id)
        {
            Order result = dbContext.Orders.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost]

        public IActionResult Add([FromBody] Order order)
        {
            order.Id = 0;
            dbContext.Orders.Add(order);
            dbContext.SaveChanges();
            return CreatedAtAction("Getid", new
            {
                id = order.Id
            }, order);
        }

        [HttpPut("{id}")]

        public IActionResult Update([FromBody] Order order, int id)
        {
            if (order.Id != id)
            {
                return BadRequest();
            }
            else if (!dbContext.Orders.Any(order => order.Id == id))
            {
                return NotFound();
            }
            else
            {
                dbContext.Orders.Update(order);
                dbContext.SaveChanges();
                return NoContent();
            }
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            Order result = dbContext.Orders.FirstOrDefault(o => o.Id == id);
            if (result == null)
            {
                return NotFound(result);
            }
            dbContext.Orders.Remove(result);
            dbContext.SaveChanges();
            return NoContent();
        }

    }
        
        
}
