using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTO
{
    public class EventOrganizerDTO
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public int OrganaizerId { get; set; }

        public OrganizerDTO Organizer { get; set; }

        public EventDTO Event { get; set; }
    }
}
