using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind_API.Models.API;
using Northwind_API.Models.Entities;
using Northwind_API.Services.Interfaces;

namespace Northwind_API.Controllers
{
    [Route("api/orders")]
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

                if (allOrders == null)
                {
                    var notFoundResponse = new ApiResponse<string>
                    {
                        Success = false,
                        Message = "No orders could be found."
                    };

                    return new NotFoundObjectResult(notFoundResponse);
                }

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


        [HttpGet("{Id}")]
        public async Task<ActionResult<Order>> GetOrderById(short Id)
        {
            try
            {
                var orderById = await _orderService.GetOrderById(Id);

                if (orderById == null)
                {
                    var notFoundResponse = new ApiResponse<string>
                    {
                        Success = false,
                        Message = $"Order with Id {Id} not found."
                    };

                    return new NotFoundObjectResult(notFoundResponse);
                }


                var response = new ApiResponse<Order>
                {
                    Success = true,
                    Message = $"Order with Id {Id} found.",
                    Data = orderById
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
