using System;
namespace DtoLayer.RoleDtos
{
	public class RoleAssignModel
	{


        public RoleAssignModel()
        {
            Roles = new List<RoleUpdateDto>();
        }

        public string userId { get; set; }

        public List<RoleUpdateDto> Roles { get; set; }


    }
}

