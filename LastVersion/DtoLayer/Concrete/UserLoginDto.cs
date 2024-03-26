﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DtoLayer.Concrete
{
	public class UserLoginDto
	{
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}

