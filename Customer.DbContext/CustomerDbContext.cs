using Customer.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using Oracle.ManagedDataAccess.Client;

namespace Customer.DatabaseContext
{   
    public class CustomerDbContext : DbContext
    {
        private readonly CustomerDbModel _options;
        private readonly DbContextOptions<CustomerDbContext> _dbContextOptions;

        public CustomerDbContext(IOptions<CustomerDbModel> options, DbContextOptions<CustomerDbContext> dbContext) : base(dbContext)
        {
            _options = options.Value;
            _dbContextOptions = dbContext;

        }
        public OracleConnection CreateConnection()
            => new OracleConnection(_options.ConnectionString);

        public DbSet<GeneralResponse<CustomerResponseModel>> Customers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerRequestModel>()
                .HasIndex(x => x.Email)
                .IsUnique();

            modelBuilder.Entity<CustomerRequestModel>()
                .HasIndex(x => new { x.FirstName, x.LastName, x.DateOfBirth })
                .IsUnique();
        }
    }
}
