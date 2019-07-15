using System;
using Convey.CQRS.Events;

namespace Pacco.Services.Customers.Application.Events.Rejected
{
    [Contract]
    public class CompleteCustomerRegistrationRejected : IRejectedEvent
    {
        public Guid Id { get; }
        public string Reason { get; }
        public string Code { get; }

        public CompleteCustomerRegistrationRejected(Guid id, string reason, string code)
        {
            Id = id;
            Reason = reason;
            Code = code;
        }
    }
}