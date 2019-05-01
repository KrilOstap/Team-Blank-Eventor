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

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
