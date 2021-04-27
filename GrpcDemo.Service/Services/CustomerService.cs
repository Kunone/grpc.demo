using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcDemo.Service.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ILogger<CustomerService> logger)
        {
            this._logger = logger;
        }

        public override Task<CustomerModel> GetCustomer(CustomerFindoneModel request, ServerCallContext context)
        {
            CustomerModel customer = new();

            if(request.UserId == 1)
            {
                customer.FirstName = "Kun";
                customer.LastName = "Wang";
            } else if(request.UserId == 2)
            {
                customer.FirstName = "Dani";
                customer.LastName = "Huang";
            } else
            {
                customer.FirstName = "Ethan";
                customer.LastName = "Wang";
            }

            return Task.FromResult(customer);
        }

        public override async Task GetNewCustomers(
            CustomerFindAllModel request,
            IServerStreamWriter<CustomerModel> responseStream,
            ServerCallContext context)
        {
            List<CustomerModel> customers = new()
            {
                new CustomerModel
                {
                    FirstName = "Daniel",
                    LastName = "Ding"
                },
                new CustomerModel
                {
                    FirstName = "Joe",
                    LastName = "Zhou"
                },
                new CustomerModel
                {
                    FirstName = "Marry",
                    LastName = "Ma"
                }
            };

            foreach (var customer in customers)
            {
                await responseStream.WriteAsync(customer);
                await Task.Delay(3000);
            }
        }
    }
}
