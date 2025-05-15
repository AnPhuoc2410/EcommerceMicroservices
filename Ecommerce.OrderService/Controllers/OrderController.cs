using Ecommerce.Model;
using Ecommerce.OrderService.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Ecommerce.OrderService.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController(OrderDbContext dbContext) : ControllerBase
    {
        [HttpGet]
        public async Task<List<OrderModel>> GetOrders()
        {
            return await dbContext.Orders.ToListAsync();
        }

        [HttpPost]
        public async Task<OrderModel> CreateOrder(OrderModel order)
        {
            order.OrderDate = DateTime.Now;
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync();

            return order;
        }
    }
}
