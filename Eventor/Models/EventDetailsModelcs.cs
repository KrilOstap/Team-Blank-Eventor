using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventor.Models
{
    public class EventDetailsModel
    {
        public string Id { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public int Number { get; set; }

        public bool IsSubscribed { get; set; }

        public bool IsOwner { get; set; }

        public string OrganizerId { get; set; }

        public ApplicationUserDTO Organizer { get; set; }
    }
}
