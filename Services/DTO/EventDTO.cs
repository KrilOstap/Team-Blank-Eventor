using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTO
{
    public class EventDTO
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string City { get; set; }

        public string Addres { get; set; }

        public int Number { get; set; }

        public int OrganizerId { get; set; }

        public IEnumerable<EventOrganizerDTO> EventOrganaizers { get; set; }
    }
}
