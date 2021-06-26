using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Dtos
{
    public class ClientUpdateDto
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name must have a maximum of 100 characters")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email has invalid format")]
        [StringLength(100, ErrorMessage = "Email must have a maximum of 100 characters")]
        public string Email { get; set; }
    }
}
