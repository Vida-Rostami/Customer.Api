using Customer.Domain;
using Customer.Services;
using System.Text.RegularExpressions;

namespace Customer.Validation
{
    public interface ICustomerValidation
    {
        Task<GeneralResponse<CustomerResponseModel>> GetCustomerById(int id);
        Task<GeneralResponse<CustomerResponseModel>> AddCustomer(CustomerRequestModel model);
        Task<GeneralResponse<CustomerResponseModel>> UpdateCustomer(int customerId, CustomerRequestModel model);

    }
    public class CustomerValidation : ICustomerValidation
    {
        private readonly ICustomerService _customerService;

        public CustomerValidation(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<GeneralResponse<CustomerResponseModel>> GetCustomerById(int id)
        {
            var response =await _customerService.GetCustomerById(id);
            if(response == null)
            {
                return new GeneralResponse<CustomerResponseModel>
                {
                    Message = "با id وارد شده مشتری یافت نشد."
                };
            }
            return response;
        }
        public async Task<GeneralResponse<CustomerResponseModel>> AddCustomer(CustomerRequestModel model)
        {
            if (string.IsNullOrEmpty(model.PhoneNumber))
            {
                return new GeneralResponse<CustomerResponseModel>
                {
                    IsSuccess = false,
                    Message = "شماره تلفن نمی‌تواند خالی باشد."
                };
            }


            Regex phoneNumberRegex = new Regex(@"(09)9[0-9]");
            bool isValidPhoneNumber = phoneNumberRegex.IsMatch(model.PhoneNumber);
            if(isValidPhoneNumber)
            {
                return await _customerService.AddCustomer(model);
            }
            else
            {
                return new GeneralResponse<CustomerResponseModel>
                {
                    IsSuccess = false,
                    Message = "فرمت شماره تلغن اشتباه است."
                };
            }
        }

        public async Task<GeneralResponse<CustomerResponseModel>> UpdateCustomer(int customerId, CustomerRequestModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                return new GeneralResponse<CustomerResponseModel>
                {
                    IsSuccess = false,
                    Message = "ایمیل نمی‌تواند خالی باشد."
                };
            }

            if (string.IsNullOrEmpty(model.BankAccountNumber))
            {
                return new GeneralResponse<CustomerResponseModel>
                {
                    IsSuccess = false,
                    Message = "شماره شبا نمی‌تواند خالی باشد."
                };
            }

            Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            bool isValidEmail = emailRegex.IsMatch(model.Email);

            Regex bankAccountNumberRegex = new Regex(@"^(?:IR)(?=.{24}$)[0-9]*$");
            bool isValidBankAccountNumber = bankAccountNumberRegex.IsMatch(model.BankAccountNumber);

            if (!isValidEmail)
            {   
                return new GeneralResponse<CustomerResponseModel>
                {
                    IsSuccess = false,
                    Message = "فرمت ایمیل اشتباه وارد شده است."
                };
            }
            
            if (!isValidBankAccountNumber)
            {
                return new GeneralResponse<CustomerResponseModel>
                {
                    IsSuccess = false,
                    Message = "فرمت  شماره شبا اشتباه وارد شده است."
                };
            }

            return await _customerService.UpdateCustomer(customerId, model);

        }
    }
}
