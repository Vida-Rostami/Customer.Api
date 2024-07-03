using Customer.Domain;
using Customer.Services;
using Customer.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerValidation _customerValidation;
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerValidation customerValidation, ICustomerService customerService)
        {
            _customerValidation = customerValidation;
            _customerService = customerService;
        }

        [HttpGet]
        Task<GeneralResponse<List<CustomerResponseModel>>> GetAllCustomer()
        {
            return _customerService.GetAllCustomer();

        }

        [HttpGet]
        Task<GeneralResponse<CustomerResponseModel>> GetCustomerById(int id)
        {
            return _customerValidation.GetCustomerById(id);
        }

        [HttpPost]
        Task<GeneralResponse<CustomerResponseModel>> AddCustomer(CustomerRequestModel model)
        {
            return _customerValidation.AddCustomer(model);
        }

        [HttpPut]
        Task<GeneralResponse<CustomerResponseModel>> UpdateCustomer(int customerId, CustomerRequestModel model)
        {
            return _customerValidation.UpdateCustomer(customerId, model);
        }

        [HttpDelete]
        Task<GeneralResponse<CustomerResponseModel>> DeleteCustomer(int customerId)
        {
            return _customerService.DeleteCustomer(customerId);
        }
    }
}
