using Convey.Exceptions;
using System;

namespace Pacco.Services.Customers.Core.Exceptions
{
    public class CustomerNotFoundException : DomainException
    {
        public override string Code => "customer_not_found";
        public Guid Id { get; }

        public CustomerNotFoundException(Guid id) : base($"Customer with id: {id} was not found.")
        {
            Id = id;
        }
    }
}