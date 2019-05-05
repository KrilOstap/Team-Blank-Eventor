using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTO
{
    public class EmailDTO
    {
        public string EventId { get; set; }

        public string Subject { get; set; }
       
        public string Body { get; set; }
    }
}
