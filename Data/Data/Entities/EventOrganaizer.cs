using Eventor.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Data.Entities
{
    public class EventOrganaizer
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public int OrganaizerId { get; set; }

        public Organizer Organizer { get; set; }

        public Event Event { get; set; }
    }
}
