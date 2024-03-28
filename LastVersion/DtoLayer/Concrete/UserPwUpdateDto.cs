using System;
using System.ComponentModel.DataAnnotations;

namespace DtoLayer.Concrete
{
	public class UserPwUpdateDto
	{
        [Required(ErrorMessage ="Please type Current pw")]
        public string CurrentPw { get; set; }


        [Required(ErrorMessage = "Please type new pw")]
        [MinLength(8)]
        public string NewPw { get; set; }

        public string  username { get; set; }
    }
}

