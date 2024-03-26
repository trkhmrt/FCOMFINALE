using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Concrete
{
	public class Role:IdentityRole<Guid>
    { 
       
      

    }
}

