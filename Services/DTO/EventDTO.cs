using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.DTO
{
    public class EventDTO
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Title")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Title length should be between 2 and 20")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Description")]
        [StringLength(1000, MinimumLength = 20, ErrorMessage = "Title length should be between 20 and 1000")]
        public string Description { get; set; }
        
        public string ImagePath { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the City")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Title length should be between 3 and 30")]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Address")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Title length should be between 3 and 30")]
        public string Address { get; set; }

        [Range(1, 1000, ErrorMessage = "Title number should be between 1 and 1000")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Number")]        
        public int Number { get; set; }

        public string OrganizerId { get; set; }

        public ApplicationUserDTO Organizer { get; set; }

        ICollection<SubscriptionDTO> Subscriptions { get; set; }
    }
}
