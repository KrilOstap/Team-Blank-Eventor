﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTO
{
    public class EventDTO
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

        public ApplicationUserDTO Organizer { get; set; }

        ICollection<SubscriptionDTO> Subscriptions { get; set; }
    }
}
