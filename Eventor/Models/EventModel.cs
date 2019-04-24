using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventor.Models
{
    public class EventModel
    {
        public string Id { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string FulllAddres { get; set; }
        
        public string OrganizerId { get; set; }

        //public ApplicationUser Organizer { get; set; }

        //ICollection<Subscription> Subscriptions { get; set; }
    }
}
