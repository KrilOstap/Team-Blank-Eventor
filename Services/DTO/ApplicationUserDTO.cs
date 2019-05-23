using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTO
{
    public class ApplicationUserDTO
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool AppliedForPromotion { get; set; }
    }
}
