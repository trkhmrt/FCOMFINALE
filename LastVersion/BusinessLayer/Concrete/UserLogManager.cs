using System;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
	public class UserLogManager:IUserLogService
	{
        private readonly IUserLogDal _userLogDal;

		public UserLogManager(IUserLogDal userLogDal)
		{
            _userLogDal = userLogDal;
		}

        public void TDelete(UserLog t)
        {
            _userLogDal.Delete(t);
        }

        public UserLog TGetByID(Guid id)
        {
            return _userLogDal.GetByID(id);
        }

        public List<UserLog> TGetList()
        {
            return _userLogDal.GetList();
        }

        public void TInsert(UserLog t)
        {
            _userLogDal.Insert(t);
        }

        public void TUpdate(UserLog t)
        {
            _userLogDal.Update(t);
        }
    }
}

