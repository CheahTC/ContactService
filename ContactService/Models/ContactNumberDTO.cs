using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactService.Models
{
    public class ContactNumberDTO
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string ContactPersonName { get; set; }
        public bool Active { get; set; }
    }
}