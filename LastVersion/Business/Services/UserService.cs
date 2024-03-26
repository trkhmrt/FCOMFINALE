using System;
using Microsoft.AspNetCore.Http;

namespace Business.Services
{
	public class UserService:IUserService
	{

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetLoginUserName()
        {
            return _httpContextAccessor.HttpContext.User.Identity.Name;
        }

    }
}

