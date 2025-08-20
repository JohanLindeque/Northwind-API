using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind_API.Models.API;
using Northwind_API.Models.Entities;
using Northwind_API.Services.Interfaces;

namespace Northwind_API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            try
            {
                var allOrders = await _orderService.GetAllOrders();

                var response = new ApiResponse<List<Order>>
                {
                    Success = true,
                    Message = "All orders returned.",
                    Data = allOrders
                };

                return new OkObjectResult(response);
            }
            catch (System.Exception ex)
            {

                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "An error occurred.",
                    Data = ex.Message
                };

                return new BadRequestObjectResult(response);
            }
        }


    }
}
