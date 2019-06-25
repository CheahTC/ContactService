using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ContactService.Models
{
    public class ContactPerson
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        
    }
}