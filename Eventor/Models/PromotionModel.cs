using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventor.Models
{
    public class PromotionModel
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(30, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(30, MinimumLength = 2)]
        public string LastName { get; set; }

        public string Password { get; set; }

        public bool LogginFailed { get; set; }
    }
}
