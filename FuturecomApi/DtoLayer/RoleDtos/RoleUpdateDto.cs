using System;
namespace DtoLayer.RoleDtos
{
	public class RoleUpdateDto
	{
        public Guid RoleID { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public bool Exists { get; set; }
    }
}

