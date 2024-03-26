using System;
using BusinessLayer.Abstract.IGenericUserServices;
using DataAccessLayer.Abstract.IGenericUser;
using DtoLayer.LoginDto;
using DtoLayer.UserDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;

namespace BusinessLayer.Concrete
{
	public class UserManagement:IUserService
	{
        IUserDal _userDal;

        public UserManagement(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void TDelete(User t)
        {
            throw new NotImplementedException();
        }

        public User TGetByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<User> TGetList()
        {
            throw new NotImplementedException();
        }

        public void TInsert(User t)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(User t)
        {
            throw new NotImplementedException();
        }
    }
}

