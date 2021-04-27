using System;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcDemo.Service;

Console.WriteLine("Enter return to start");
Console.ReadLine();

var channel = GrpcChannel.ForAddress("http://localhost:5000");
var client = new Greeter.GreeterClient(channel);
var customerClient = new Customer.CustomerClient(channel);

var response = await client.SayHelloAsync(new HelloRequest { Name = "kun" });
Console.WriteLine(response.Message);

var customerResponse = await customerClient.GetCustomerAsync(new CustomerFindoneModel { UserId = 1 });
Console.WriteLine(customerResponse.FirstName);

using (var call = customerClient.GetNewCustomers(new CustomerFindAllModel()))
{
    while (await call.ResponseStream.MoveNext())
    {
        var customer = call.ResponseStream.Current;
        Console.WriteLine($"Added {customer.FirstName} {customer.LastName}");
    }
};

Console.ReadLine();

