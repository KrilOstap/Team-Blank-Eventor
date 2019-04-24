using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTO
{
    public class SubscriptionDTO
    {
        public string Id { get; set; }

        public string EventId { get; set; }

        public EventDTO Event { get; set; }

        public string OrganizerId { get; set; }

        public ApplicationUserDTO Organizer { get; set; }
    }
}
