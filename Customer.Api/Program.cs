
using Customer.DatabaseContext;
using Customer.Domain;
using Customer.Repository;
using Customer.Services;
using Customer.Validation;
using Microsoft.Extensions.Options;
using Oracle.EntityFrameworkCore;

namespace Customer.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHealthChecks();


            builder.Services.Configure<CustomerDbModel>(builder.Configuration.GetSection("CustomerDbSettings"));

            builder.Services.AddDbContext<CustomerDbContext>();

            
            builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
            builder.Services.AddSingleton<ICustomerService, CustomerService>();
            builder.Services.AddSingleton<ICustomerValidation, CustomerValidation>();
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHealthChecks("/health");
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
