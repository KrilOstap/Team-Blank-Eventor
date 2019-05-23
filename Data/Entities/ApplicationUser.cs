using DataAccess.Data.Entities;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventor.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {       
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool AppliedForPromotion { get; set; }

        ICollection<Event> Events { get; set; }

        ICollection<Subscription> Subscriptions { get; set; }
    }
}
