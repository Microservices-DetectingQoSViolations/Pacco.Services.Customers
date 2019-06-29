using System;
using Convey;
using Convey.CQRS.Queries;
using Convey.Discovery.Consul;
using Convey.HTTP;
using Convey.LoadBalancing.Fabio;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Persistence.MongoDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pacco.Services.Customers.Application.Commands;
using Pacco.Services.Customers.Application.Events.External;
using Pacco.Services.Customers.Application.Services;
using Pacco.Services.Customers.Core.Repositories;
using Pacco.Services.Customers.Core.Services;
using Pacco.Services.Customers.Infrastructure.Mongo.Documents;
using Pacco.Services.Customers.Infrastructure.Mongo.Repositories;
using Pacco.Services.Customers.Infrastructure.Services;

namespace Pacco.Services.Customers.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddSingleton<IEventMapper, EventMapper>();
            builder.Services.AddTransient<IMessageBroker, MessageBroker>();
            builder.Services.AddTransient<ICustomerRepository, CustomerMongoRepository>();
            builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            builder.Services.AddSingleton<IVipPolicy, VipPolicy>();

            return builder
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddHttpClient()
                .AddConsul()
                .AddFabio()
                .AddRabbitMq()
                .AddMongo()
                .AddMongoRepository<CustomerDocument, Guid>("Customers");
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseInitializers()
                .UseRabbitMq()
                .SubscribeCommand<CreateCustomer>()
                .SubscribeEvent<SignedUp>()
                .SubscribeEvent<OrderCompleted>();

            return app;
        }
    }
}