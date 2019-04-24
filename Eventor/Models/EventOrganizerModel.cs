using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventor.Models
{
    public class EventOrganizerModel
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public int OrganaizerId { get; set; }

        public OrganizerModel Organizer { get; set; }

        public EventModel Event { get; set; }
    }
}
