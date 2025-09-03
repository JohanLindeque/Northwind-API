using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Northwind_API.Helpers;
using Northwind_API.Models.Entities;
using Northwind_API.Models.Models;
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
            var correlationId = ApiHelper.GenerateCorrelationId();
            try
            {
                var allOrders = await _orderService.GetAllOrders();

                if (allOrders == null)
                    return new NotFoundObjectResult(ApiResponse<List<Order>>.ErrorResult(correlationId, "No orders could be found.", new List<Order>()));


                return new OkObjectResult(ApiResponse<List<Order>>.Result(correlationId, "All orders returned.", allOrders));
            }
            catch (System.Exception ex)
            {
                return new BadRequestObjectResult(ApiResponse<string>.ErrorResult(correlationId, "An error occurred.", ex.Message));
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
                        Message = $"Order with Id {Id} was not found."
                    };

                    return new NotFoundObjectResult(notFoundResponse);
                }


                var response = new ApiResponse<Order>
                {
                    Success = true,
                    Message = $"Order with Id {Id} found.",
                    Response = orderById
                };

                return new OkObjectResult(response);
            }
            catch (System.Exception ex)
            {

                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "An error occurred.",
                    Response = ex.Message
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
                        Response = ModelState
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
                    Response = createdOrder
                };


                return CreatedAtAction(nameof(GetOrderById), new { Id = createdOrder.OrderId }, response);
            }
            catch (System.Exception ex)
            {

                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "An error occurred.",
                    Response = ex.Message
                };

                return new BadRequestObjectResult(response);
            }
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult<string>> DeleteOrderById(short Id)
        {
            try
            {
                var wasDeleted = await _orderService.DeleteOrderById(Id);

                if (!wasDeleted)
                {
                    var notDeletedResponse = new ApiResponse<string>
                    {
                        Success = false,
                        Message = $"Order with Id {Id} was not found."
                    };

                    return new NotFoundObjectResult(notDeletedResponse);
                }


                return NoContent();
            }
            catch (System.Exception ex)
            {

                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "An error occurred.",
                    Response = ex.Message
                };

                return new BadRequestObjectResult(response);
            }
        }



    }
}
