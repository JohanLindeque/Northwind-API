using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Northwind_API.Helpers;
using Northwind_API.Models.Api;
using Northwind_API.Models.Models;
using Northwind_API.Services.Interfaces;
using Serilog;

namespace Northwind_API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private IOrder _orderService;
        public OrderController(IOrder orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("all"), Authorize]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            var correlationId = ApiHelper.GenerateCorrelationId();
            Log.Information("[{correlationId}], GetAllOrders, Request recieved.", correlationId);

            try
            {
                var allOrders = await _orderService.GetAllOrders();

                if (allOrders == null)
                {
                    Log.Warning("[{correlationId}], GetAllOrders, No Orders could be found.", correlationId);
                    return new NotFoundObjectResult(ApiResponse<string>.ErrorResult(correlationId, "No orders could be found.", string.Empty));
                }

                Log.Information("[{correlationId}], GetAllOrders, All Orders returned.", correlationId);
                return new OkObjectResult(ApiResponse<List<Order>>.Result(correlationId, "All orders returned.", allOrders));
            }
            catch (System.Exception ex)
            {
                Log.Error("[{correlationId}], GetAllOrders, An Error occurred: {error}.", correlationId, ex.Message);
                return new BadRequestObjectResult(ApiResponse<string>.ErrorResult(correlationId, "An error occurred.", ex.Message));
            }
        }


        [HttpGet("{Id}"), Authorize]
        public async Task<ActionResult<Order>> GetOrderById(short Id)
        {
            var correlationId = ApiHelper.GenerateCorrelationId();
            Log.Information("[{correlationId}], GetOrdersById, Request recieved.", correlationId);


            try
            {
                var orderById = await _orderService.GetOrderById(Id);

                if (orderById == null)
                {
                    Log.Information("[{correlationId}], GetOrderById, Order with Id {id} was not found.", correlationId, Id);
                    return new NotFoundObjectResult(ApiResponse<string>.ErrorResult(correlationId, $"Order with Id {Id} was not found.", string.Empty));
                }

                Log.Information("[{correlationId}], GetOrderById, Order with Id {id} was found.", correlationId, Id);
                return new OkObjectResult(ApiResponse<Order>.Result(correlationId, $"Order with Id {Id} found.", orderById));

            }
            catch (System.Exception ex)
            {
                Log.Error("[{correlationId}], GetOrdedrById, An Error occurred: {error}.", correlationId, ex.Message);
                return new BadRequestObjectResult(ApiResponse<string>.ErrorResult(correlationId, "An error occurred.", ex.Message));

            }
        }


        [HttpPost("add"), Authorize]
        public async Task<ActionResult<Order>> CreateOrder(Order newOrder)
        {
            var correlationId = ApiHelper.GenerateCorrelationId();
            Log.Information("[{correlationId}], GetOrdersById, Request recieved.", correlationId);

            try
            {
                if (!ModelState.IsValid)
                {
                    Log.Fatal("[{correlationId}], CreateOrder, Invalid Model state: {error}.", correlationId, ModelState);
                    return new BadRequestObjectResult(ApiResponse<ModelStateDictionary>.ErrorResult(correlationId, $"An error occurred.", ModelState));
                }


                if (newOrder == null)
                {
                    Log.Error("[{correlationId}], CreateOrder, Order can not be null.", correlationId);
                    return new BadRequestObjectResult(ApiResponse<string>.ErrorResult(correlationId, $"Order can not be null.", string.Empty));
                }



                var createdOrder = await _orderService.CreateNewOrder(newOrder);

                Log.Information("[{correlationId}], CreateOrder, Order with Id {id} created successfully.", correlationId, createdOrder.OrderId);
                return CreatedAtAction(nameof(GetOrderById), new { Id = createdOrder.OrderId }, ApiResponse<Order>.Result(correlationId, $"Order with Id {createdOrder.OrderId} created successfully.", createdOrder));

            }
            catch (System.Exception ex)
            {
                Log.Error("[{correlationId}], CreateOrder, An Error occurred: {error}.", correlationId, ex.Message);
                return new BadRequestObjectResult(ApiResponse<string>.ErrorResult(correlationId, "An error occurred.", ex.Message));
            }
        }


        [HttpDelete("{Id}"), Authorize]
        public async Task<ActionResult<string>> DeleteOrderById(short Id)
        {
            var correlationId = ApiHelper.GenerateCorrelationId();
            Log.Information("[{correlationId}], DeleteOrderById, Request recieved.", correlationId);

            try
            {
                var wasDeleted = await _orderService.DeleteOrderById(Id);

                if (!wasDeleted)
                {
                    Log.Information("[{correlationId}], DeleteOrderById, Order with Id {id} was not found.", correlationId, Id);
                    return new NotFoundObjectResult(ApiResponse<string>.ErrorResult(correlationId, $"Order with Id {Id} was not found.", string.Empty));
                }

                Log.Information("[{correlationId}], DeleteOrderById, Order with Id {id} created successfully.", correlationId, Id);
                return new ObjectResult(ApiResponse<string>.Result(correlationId, $"Order with Id {Id} was deleted.", string.Empty)) { StatusCode = StatusCodes.Status204NoContent };
            }
            catch (System.Exception ex)
            {
                Log.Error("[{correlationId}], DeletOrerById, An Error occurred: {error}.", correlationId, ex.Message);
                return new BadRequestObjectResult(ApiResponse<string>.ErrorResult(correlationId, "An error occurred.", ex.Message));
            }
        }


        [HttpPut("{Id}"), Authorize]
        public async Task<ActionResult<Order>> UpdateOrder(short Id, Order order)
        {
            var correlationId = ApiHelper.GenerateCorrelationId();
            Log.Information("[{correlationId}], UpdateOrder, Request recieved.", correlationId);

            try
            {
                if (order == null)
                {
                    Log.Error("[{correlationId}], UpdateOrder, Order can not be null.", correlationId);
                    return new BadRequestObjectResult(ApiResponse<string>.ErrorResult(correlationId, $"Order can not be null.", string.Empty));
                }

                if (Id != order.OrderId)
                {
                    Log.Error("[{correlationId}], UpdateOrder, ID mismatch.", correlationId);
                    return new BadRequestObjectResult(ApiResponse<string>.ErrorResult(correlationId, $"ID mismatch.", string.Empty));
                }


                var updatedOrder = await _orderService.UpdateOrder(order);
                if (order == null)
                {
                    Log.Information("[{correlationId}], UpdateOrder, Order with Id {id} was not found.", correlationId, Id);
                    return new NotFoundObjectResult(ApiResponse<string>.ErrorResult(correlationId, $"Order with Id {Id} was not found.", string.Empty));
                }

                Log.Information("[{correlationId}], UpdateOrder, Order with Id {id} updated successfully.", correlationId, updatedOrder.OrderId);
                return new OkObjectResult(ApiResponse<Order>.Result(correlationId, $"Order with Id {updatedOrder.OrderId} updated successfully.", updatedOrder));

            }
            catch (System.Exception ex)
            {
                Log.Error("[{correlationId}], UpdateOrder, An Error occurred: {error}.", correlationId, ex.Message);
                return new BadRequestObjectResult(ApiResponse<string>.ErrorResult(correlationId, "An error occurred.", ex.Message));
            }
        }


    }
}
