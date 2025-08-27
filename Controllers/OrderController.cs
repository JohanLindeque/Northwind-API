using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<Order>> CreateOrder(Order newOrder)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var stateResponse = new ApiResponse<ModelStateDictionary>
                    {
                        Success = false,
                        Message = "An error occurred.",
                        Data = ModelState
                    };
                    return new BadRequestObjectResult(stateResponse);

                }
                if (newOrder == null)
                {
                    var nullResponse = new ApiResponse<ModelStateDictionary>
                    {
                        Success = false,
                        Message = "Order cannot be null."
                    };
                    return new BadRequestObjectResult(nullResponse);
                }



                var createdOrder = await _orderService.CreateNewOrder(newOrder);

                var response = new ApiResponse<Order>
                {
                    Success = true,
                    Message = $"Order with Id {createdOrder.OrderId} created successfully.",
                    Data = createdOrder
                };


                return CreatedAtAction(nameof(GetOrderById), new { Id = createdOrder.OrderId }, response );
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
