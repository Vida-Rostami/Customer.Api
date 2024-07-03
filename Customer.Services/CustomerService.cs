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
        public async Task<GeneralResponse<CustomerResponseModel>> GetCustomerById(int id)
        {
            try
            {
                return await (_customerRepository.GetCustomerById(id));

            }   
            catch (Exception ex)
            {
                return new GeneralResponse<CustomerResponseModel>
                {
                    IsSuccess = false,
                    Message = $"Message: {ex.Message}, Stack trace: {ex.StackTrace}"
                };
            }        }
        public async Task<GeneralResponse<CustomerResponseModel>> AddCustomer(CustomerRequestModel model)
        {
            try
            {
                var smallerPhoneNumber = model.PhoneNumber.Substring(2, 9);
                model.PhoneNumber = smallerPhoneNumber;
                return await _customerRepository.AddCustomer(model);
            }
            catch (Exception ex)
            {
                return new GeneralResponse<CustomerResponseModel>
                {
                    IsSuccess = false,
                    Message = $"Message: {ex.Message}, Stack trace: {ex.StackTrace}" 
                };
            }
        }
        public async Task<GeneralResponse<CustomerResponseModel>> UpdateCustomer(int customerId, CustomerRequestModel model)
        {
            try
            {
                var smallerPhoneNumber = model.PhoneNumber.Substring(2, 9);
                model.PhoneNumber = smallerPhoneNumber;
                return await _customerRepository.UpdateCustomer(customerId, model);
            }
            catch (Exception ex)    
            {
                return new GeneralResponse<CustomerResponseModel>
                {
                    IsSuccess = false,
                    Message = $"Message: {ex.Message}, Stack trace: {ex.StackTrace}"
                };
            }
        }
        public async Task<GeneralResponse<CustomerResponseModel>> DeleteCustomer(int customerId)
        {
            try
            {
                return await _customerRepository.DeleteCustomer(customerId);
            }   
            catch (Exception ex)
            {
                return new GeneralResponse<CustomerResponseModel>
                {
                    IsSuccess = false,
                    Message = $"Message: {ex.Message}, Stack trace: {ex.StackTrace}"
                };
            }
        }
    }
}