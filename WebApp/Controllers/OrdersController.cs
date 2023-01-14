﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<OrderDto> Get(int id)
        {
            var result = await _orderService.GetByIdAsync(id);
            return result;
        }
    }
}
