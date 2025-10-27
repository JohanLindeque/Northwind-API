using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind_API.Services.Interfaces;
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
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomer _customerService;
        public CustomerController(ICustomer customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("all"), Authorize]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            var correlationId = ApiHelper.GenerateCorrelationId();
            Log.Information("[{correlationId}], GetAllCustomers, Request recieved.", correlationId);

            try
            {
                var allCustomers = await _customerService.GetAllCustomers();

                if (allCustomers == null)
                {
                    Log.Warning("[{correlationId}], GetAllCustomers, No Customers could be found.", correlationId);
                    return new NotFoundObjectResult(ApiResponse<string>.ErrorResult(correlationId, "No customers could be found.", string.Empty));
                }

                Log.Information("[{correlationId}], GetAllCustomers, All customers returned.", correlationId);
                return new OkObjectResult(ApiResponse<List<Customer>>.Result(correlationId, "All customers returned.", allCustomers));
            }
            catch (System.Exception ex)
            {
                Log.Error("[{correlationId}], GetAllCustomers, An Error occurred: {error}.", correlationId, ex.Message);
                return new BadRequestObjectResult(ApiResponse<string>.ErrorResult(correlationId, "An error occurred.", ex.Message));
            }
        }

        [HttpGet("{Id}"), Authorize]
        public async Task<ActionResult<Customer>> GetCustomerById(short Id)
        {
            var correlationId = ApiHelper.GenerateCorrelationId();
            Log.Information("[{correlationId}], GetCustomerById, Request recieved.", correlationId);

            try
            {
                var customerById = await _customerService.GetCustomerById(Id);

                if (customerById == null)
                {
                    Log.Information("[{correlationId}], GetCustomerById, Customer with Id {id} was not found.", correlationId, Id);
                    return new NotFoundObjectResult(ApiResponse<string>.ErrorResult(correlationId, $"Customer with Id {Id} was not found.", string.Empty));
                }

                Log.Information("[{correlationId}], GetCustomerById, Customer with Id {id} was found.", correlationId, Id);
                return new OkObjectResult(ApiResponse<Customer>.Result(correlationId, $"Customer with Id {Id} found.", customerById));

            }
            catch (System.Exception ex)
            {
                Log.Error("[{correlationId}], GetCustomerById, An Error occurred: {error}.", correlationId, ex.Message);
                return new BadRequestObjectResult(ApiResponse<string>.ErrorResult(correlationId, "An error occurred.", ex.Message));

            }
        }


        [HttpPost("add"), Authorize]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer newCustomer)
        {
            var correlationId = ApiHelper.GenerateCorrelationId();
            Log.Information("[{correlationId}], CreateCustomer, Request recieved.", correlationId);

            try
            {
                if (!ModelState.IsValid)
                {
                    Log.Fatal("[{correlationId}], CreateCustomer, Invalid Model state: {error}.", correlationId, ModelState);
                    return new BadRequestObjectResult(ApiResponse<ModelStateDictionary>.ErrorResult(correlationId, $"An error occurred.", ModelState));
                }


                if (newCustomer == null)
                {
                    Log.Error("[{correlationId}], CreateCustomer, Customer can not be null.", correlationId);
                    return new BadRequestObjectResult(ApiResponse<string>.ErrorResult(correlationId, $"Customer can not be null.", string.Empty));
                }



                var createdCustomer = await _customerService.CreateNewCustomer(newCustomer);

                Log.Information("[{correlationId}], CreateCustomer, Customer with Id {id} created successfully.", correlationId, createdCustomer.CustomerId);
                return CreatedAtAction(nameof(GetCustomerById), new { Id = createdCustomer.CustomerId }, ApiResponse<Customer>.Result(correlationId, $"Customer with Id {createdCustomer.CustomerId} created successfully.", createdCustomer));

            }
            catch (System.Exception ex)
            {
                Log.Error("[{correlationId}], CreateCustomer, An Error occurred: {error}.", correlationId, ex.Message);
                return new BadRequestObjectResult(ApiResponse<string>.ErrorResult(correlationId, "An error occurred.", ex.Message));
            }
        }

        [HttpDelete("{Id}"), Authorize]
        public async Task<ActionResult<string>> DeleteCustomerById(short Id)
        {
            var correlationId = ApiHelper.GenerateCorrelationId();
            Log.Information("[{correlationId}], DeleteCustomerById, Request recieved.", correlationId);

            try
            {
                var wasDeleted = await _customerService.DeleteCustomerById(Id);

                if (!wasDeleted)
                {
                    Log.Information("[{correlationId}], DeleteCustomerById, Customer with Id {id} was not found.", correlationId, Id);
                    return new NotFoundObjectResult(ApiResponse<string>.ErrorResult(correlationId, $"Customer with Id {Id} was not found.", string.Empty));
                }

                Log.Information("[{correlationId}], DeleteCustomerById, Customer with Id {id} deleted successfully.", correlationId, Id);
                return new ObjectResult(ApiResponse<string>.Result(correlationId, $"Customer with Id {Id} was deleted.", string.Empty)) { StatusCode = StatusCodes.Status204NoContent };
            }
            catch (System.Exception ex)
            {
                Log.Error("[{correlationId}], DeleteCustomerById, An Error occurred: {error}.", correlationId, ex.Message);
                return new BadRequestObjectResult(ApiResponse<string>.ErrorResult(correlationId, "An error occurred.", ex.Message));
            }
        }
        
        [HttpPut("{Id}"), Authorize]
        public async Task<ActionResult<Customer>> UpdateCustomer(string Id, Customer customer)
        {
            var correlationId = ApiHelper.GenerateCorrelationId();
            Log.Information("[{correlationId}], UpdateCustomer, Request recieved.", correlationId);

            try
            {
                if (customer == null)
                {
                    Log.Error("[{correlationId}], UpdateCustomer, Customer can not be null.", correlationId);
                    return new BadRequestObjectResult(ApiResponse<string>.ErrorResult(correlationId, $"Customer can not be null.", string.Empty));
                }

                if (Id != customer.CustomerId)
                {
                    Log.Error("[{correlationId}], UpdateCustomer, ID mismatch.", correlationId);
                    return new BadRequestObjectResult(ApiResponse<string>.ErrorResult(correlationId, $"ID mismatch.", string.Empty));
                }


                var updatedCustomer = await _customerService.UpdateCustomer(customer);
                if (customer == null)
                {
                    Log.Information("[{correlationId}], UpdateCustomer, Customer with Id {id} was not found.", correlationId, Id);
                    return new NotFoundObjectResult(ApiResponse<string>.ErrorResult(correlationId, $"Customer with Id {Id} was not found.", string.Empty));
                }

                Log.Information("[{correlationId}], UpdateCustomer, Customer with Id {id} updated successfully.", correlationId, updatedCustomer.CustomerId);
                return new OkObjectResult(ApiResponse<Customer>.Result(correlationId, $"Customer with Id {updatedCustomer.CustomerId} updated successfully.", updatedCustomer));

            }
            catch (System.Exception ex)
            {
                Log.Error("[{correlationId}], UpdateCustomer, An Error occurred: {error}.", correlationId, ex.Message);
                return new BadRequestObjectResult(ApiResponse<string>.ErrorResult(correlationId, "An error occurred.", ex.Message));
            }
        }

    }
}
