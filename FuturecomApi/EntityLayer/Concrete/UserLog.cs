using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Concrete
{
    public class UserLog
    {

        public Guid UserLogID { get; set; }

        [ForeignKey(name: "User")]
        public Guid UserID { get; set; }

        public User? User { get; set; }

        [StringLength(2)]
        public string Type { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}

