using DataAccess.Data.Entities;
using Eventor.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class Subscription
    {
        public string Id { get; set; }

        public string EventId { get; set; }

        public Event Event { get; set; }

        public string OrganizerId { get; set; }

        public ApplicationUser Organizer { get; set; }
    }
}
