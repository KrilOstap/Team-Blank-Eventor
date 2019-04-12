using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTO
{
    public class OrganizerDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Expirience { get; set; }

        public IEnumerable<EventOrganizerDTO> EventOrganaizers { get; set; }
    }
}
