using System;
namespace DtoLayer.Concrete
{
	public class RoleUpdateRequestDto
	{

        public RoleUpdateRequestDto()
        {
            Roles = new List<RoleUpdateDto>();
        }

        public string userId { get; set; }

        public List<RoleUpdateDto> Roles { get; set; }

    }
}

