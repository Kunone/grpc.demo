using System;
using Grpc.Net.Client;
using GrpcDemo.Service;

Console.WriteLine("Enter return to start");
Console.ReadLine();

var channel = GrpcChannel.ForAddress("http://localhost:5000");
var client = new Greeter.GreeterClient(channel);

var response = await client.SayHelloAsync(new HelloRequest { Name = "kun" });
Console.WriteLine(response.Message);

Console.ReadLine();

