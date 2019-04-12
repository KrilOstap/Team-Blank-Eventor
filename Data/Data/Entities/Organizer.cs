using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Data.Entities
{
    public class Organizer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Expirience { get; set; }

        public IEnumerable<EventOrganaizer> EventOrganaizers { get; set; }        
    }
}
