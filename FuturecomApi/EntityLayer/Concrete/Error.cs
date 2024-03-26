using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Concrete
{
	public class Error
	{

        public Guid ErrorID { get; set; }

        [StringLength(32)]
        public string Type { get; set; }

        public string StatusCode { get; set; }

        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }



    }
}

