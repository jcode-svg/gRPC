// See https://aka.ms/new-console-template for more information


using Grpc.Core;
using Grpc.Net.Client;
using gRPCServer;

var input = new HelloRequest { Name = "Nelson" };

var channel = GrpcChannel.ForAddress("http://localhost:5002");
var greeterClient = new Greeter.GreeterClient(channel);

var response = await greeterClient.SayHelloAsync(input);

Console.WriteLine(response.Message);

var customerInfoRequest = new CustomerLookupModel { UserId = 2 };

var customerClient = new Customer.CustomerClient(channel);

var customer = await customerClient.GetCustomerInfoAsync(customerInfoRequest);

Console.WriteLine($"{customer.FirstName} {customer.LastName}");

using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
{
	while (await call.ResponseStream.MoveNext())
	{
		var currentCustomer = call.ResponseStream.Current;

        Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName} : {currentCustomer.EmailAddress}");
    }
}

Console.ReadLine();
