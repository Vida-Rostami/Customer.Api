using Customer.DatabaseContext;
using Customer.Domain;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Customer.Repository
{
    public interface ICustomerRepository
    {
        Task<GeneralResponse<List<CustomerResponseModel>>> GetAllCustomer();
        Task<GeneralResponse<CustomerResponseModel>> GetCustomerById(int id);
        Task<GeneralResponse<CustomerResponseModel>> AddCustomer(CustomerRequestModel model);
        Task<GeneralResponse<CustomerResponseModel>> UpdateCustomer(int customerId,CustomerRequestModel model);
        Task<GeneralResponse<CustomerResponseModel>> DeleteCustomer(int customerId);
    }
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _context;

        public CustomerRepository(CustomerDbContext context)
        {
            _context = context;
        }
        public async Task<GeneralResponse<List<CustomerResponseModel>>> GetAllCustomer()
        {
            var response = await _context.Customers.ToListAsync();

            var customerResponseModels = response.Select(x => new CustomerResponseModel
            {
                CustomerId = x.Data.CustomerId,
                FirstName = x.Data.FirstName,
                LastName = x.Data.FirstName,
                DateOfBirth = x.Data.DateOfBirth,
                PhoneNumber = x.Data.PhoneNumber,
                Email = x.Data.Email,
                BankAccountNumber = x.Data.BankAccountNumber
            }).ToList();

            return new GeneralResponse<List<CustomerResponseModel>>
            {
                Data = customerResponseModels
            };

        }
        public async Task<GeneralResponse<CustomerResponseModel>> GetCustomerById(int id)
        {

            return await _context.Customers.FirstOrDefaultAsync(x => x.Data.CustomerId == id);

        }
        public async Task<GeneralResponse<CustomerResponseModel>> AddCustomer(CustomerRequestModel model)
        {
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
            return new GeneralResponse<CustomerResponseModel>
            {
                Data = new CustomerResponseModel
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DateOfBirth = model.DateOfBirth,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    BankAccountNumber = model.BankAccountNumber
                }
            };

        }
        public async Task<GeneralResponse<CustomerResponseModel>> UpdateCustomer(int customerId, CustomerRequestModel model)
        {

            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(x => x.Data.CustomerId == customerId);
            if (existingCustomer == null)
            {
                return null;
            }
            existingCustomer.Data.FirstName = model.FirstName;
            existingCustomer.Data.LastName = model.LastName;
            existingCustomer.Data.DateOfBirth = model.DateOfBirth;
            existingCustomer.Data.PhoneNumber = model.PhoneNumber;
            existingCustomer.Data.Email = model.Email;
            existingCustomer.Data.BankAccountNumber = model.BankAccountNumber;
            await _context.SaveChangesAsync();
            return existingCustomer;
        }
        public async Task<GeneralResponse<CustomerResponseModel>> DeleteCustomer(int customerId)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.Data.CustomerId == customerId);
        }

    }
}