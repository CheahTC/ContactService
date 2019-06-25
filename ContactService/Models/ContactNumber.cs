using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ContactService.Models
{
    public class ContactNumber
    {
        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        [Display(Name = "Activated?")]
        public bool Active { get; set; }

        // Foreign Key
        public int ContactPersonId { get; set; }
        // Navigation property
        public ContactPerson ContactPerson { get; set; }
    }
}