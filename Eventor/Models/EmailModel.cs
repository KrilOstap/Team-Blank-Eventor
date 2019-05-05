using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventor.Models
{
    public class EmailModel
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 5)]
        public string Subject { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 5)]
        public string Body { get; set; }

        public string EventId { get; set; }
    }
}
