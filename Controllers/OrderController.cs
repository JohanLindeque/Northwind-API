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
            var correlationId = ApiHelper.GenerateCorrelationId();

            try
            {
                var orderById = await _orderService.GetOrderById(Id);

                if (orderById == null)
                    return new NotFoundObjectResult(ApiResponse<Order>.ErrorResult(correlationId, $"Order with Id {Id} was not found.", new Order()));


                return new OkObjectResult(ApiResponse<Order>.Result(correlationId, $"Order with Id {Id} found.", orderById));

            }
            catch (System.Exception ex)
            {
                return new BadRequestObjectResult(ApiResponse<string>.ErrorResult(correlationId, "An error occurred.", ex.Message));

            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<Order>> CreateOrder(Order newOrder)
        {
            var correlationId = ApiHelper.GenerateCorrelationId();

            try
            {
                if (!ModelState.IsValid)
                    return new BadRequestObjectResult(ApiResponse<ModelStateDictionary>.ErrorResult(correlationId, $"An error occurred.", ModelState));


                if (newOrder == null)
                    return new BadRequestObjectResult(ApiResponse<Order>.ErrorResult(correlationId, $"Order cannot be null.", new Order()));



                var createdOrder = await _orderService.CreateNewOrder(newOrder);


                return CreatedAtAction(nameof(GetOrderById), new { Id = createdOrder.OrderId }, ApiResponse<Order>.Result(correlationId, $"Order with Id {createdOrder.OrderId} created successfully.", createdOrder));

            }
            catch (System.Exception ex)
            {
                return new BadRequestObjectResult(ApiResponse<string>.ErrorResult(correlationId, "An error occurred.", ex.Message));
            }
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult<string>> DeleteOrderById(short Id)
        {
            var correlationId = ApiHelper.GenerateCorrelationId();

            try
            {
                var wasDeleted = await _orderService.DeleteOrderById(Id);

                if (!wasDeleted)
                    return new NotFoundObjectResult(ApiResponse<string>.ErrorResult(correlationId, $"Order with Id {Id} was not found.", string.Empty));


                return new ObjectResult(ApiResponse<string>.Result(correlationId, $"Order with Id {Id} was deleted.", string.Empty)) { StatusCode = StatusCodes.Status204NoContent };
            }
            catch (System.Exception ex)
            {
                return new BadRequestObjectResult(ApiResponse<string>.ErrorResult(correlationId, "An error occurred.", ex.Message));
            }
        }



    }
}
