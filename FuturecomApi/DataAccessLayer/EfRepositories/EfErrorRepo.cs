using System;
using DataAccessLayer.Abstract;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using EntityLayer.Concrete;

namespace DataAccessLayer.EfRepositories
{
	public class EfErrorRepo: GenericRepository<Error>, IErrorDal
    {
		
	}
}

