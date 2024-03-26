using System;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.IGenericUser;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using EntityLayer.Concrete;

namespace DataAccessLayer.EfRepositories
{
	public class EfUserRepo:GenericRepository<User>,IUserDal
	{
		
	}
}

