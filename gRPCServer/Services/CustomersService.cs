using Grpc.Core;

namespace gRPCServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private ILogger<CustomersService> _logger { get; }

        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if (request.UserId == 1)
            {
                output.FirstName = "James";
                output.LastName = "Brown";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "Jane";
                output.LastName = "Doe";
            }
            else
            {
                output.FirstName = "Bill";
                output.LastName = "Smith";
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                     FirstName = "John",
                     LastName = "Smith",
                     EmailAddress = "johnsmith@gmail.com",
                     Age = 32,
                     IsAlive = true
                },
                new CustomerModel
                {
                     FirstName = "Alan",
                     LastName = "James",
                     EmailAddress = "alanjames@gmail.com",
                     Age = 36,
                     IsAlive = true
                },
                new CustomerModel
                {
                     FirstName = "Jaden",
                     LastName = "Joe",
                     EmailAddress = "jadendoe@gmail.com",
                     Age = 45,
                     IsAlive = true
                },
            };

            foreach (CustomerModel customer in customers)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(customer);
            }
        }
    }
}
