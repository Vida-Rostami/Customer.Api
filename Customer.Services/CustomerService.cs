using Customer.Domain;
using Customer.Repository;

namespace Customer.Services
{
    public interface ICustomerService
    {

        Task<GeneralResponse<List<CustomerResponseModel>>> GetAllCustomer();
        Task<GeneralResponse<CustomerResponseModel>> GetCustomerById(int id);
        Task<GeneralResponse<CustomerResponseModel>> AddCustomer(CustomerRequestModel model);
        Task<GeneralResponse<CustomerResponseModel>> UpdateCustomer(int customerId, CustomerRequestModel model);
        Task<GeneralResponse<CustomerResponseModel>> DeleteCustomer(int customerId);
    }
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<GeneralResponse<List<CustomerResponseModel>>> GetAllCustomer()
        {
            return _customerRepository.GetAllCustomer();
        }
        public Task<GeneralResponse<CustomerResponseModel>> GetCustomerById(int id)
        {
            return (_customerRepository.GetCustomerById(id));
        }
        public Task<GeneralResponse<CustomerResponseModel>> AddCustomer(CustomerRequestModel model)
        {
            var smallerPhoneNumber = model.PhoneNumber.Substring(2, 9);
            model.PhoneNumber = smallerPhoneNumber;
            return _customerRepository.AddCustomer(model);
        }
        public Task<GeneralResponse<CustomerResponseModel>> UpdateCustomer(int customerId, CustomerRequestModel model)
        {

            var smallerPhoneNumber = model.PhoneNumber.Substring(2, 9);
            model.PhoneNumber = smallerPhoneNumber;
            return _customerRepository.UpdateCustomer(customerId, model);
        }
        public Task<GeneralResponse<CustomerResponseModel>> DeleteCustomer(int customerId)
        {
            return _customerRepository.DeleteCustomer(customerId);
        }
    }
}