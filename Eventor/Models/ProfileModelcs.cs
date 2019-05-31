using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventor.Models
{
    public class ProfileModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsOwner { get; set; }

        public string ImagePath { get; set; }

        public IEnumerable<EventDTO> Events { get; set; }
    }
}
