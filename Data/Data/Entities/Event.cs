using DataAccess.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Data.Entities
{
    public class Event
    {        
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string City { get; set; }

        public string Addres { get; set; }

        public int Number { get; set; }

        public int OrganizerId { get; set; }

        public IEnumerable<EventOrganaizer> EventOrganaizers { get; set; }
    }
}
