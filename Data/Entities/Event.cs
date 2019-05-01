using DataAccess.Data.Entities;
using DataAccess.Entities;
using Eventor.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Data.Entities
{
    public class Event
    {        
        public string Id { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public int Number { get; set; }

        public string OrganizerId { get; set; }

        public ApplicationUser Organizer { get; set; }

        ICollection<Subscription> Subscriptions { get; set; }       
    }
}
