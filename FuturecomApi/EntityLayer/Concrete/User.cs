using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Concrete
{
	public class User:IdentityUser<Guid>
	{


        [StringLength(128)]
        public string Email { get; set; }

        [StringLength(32)]
        public string FirstName { get; set; }

        [StringLength(64)]
        public string LastName { get; set; }

        [StringLength(16)]
        public string PhoneNumber { get; set; }

        public bool Status { get; set; }







    }
}

