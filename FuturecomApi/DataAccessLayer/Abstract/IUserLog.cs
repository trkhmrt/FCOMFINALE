using System;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IUserLogDal : IGenericDal<UserLog>
    {
        void TInsert(string type, string userId);

        List<UserLog> GetUserLog(); 

    }
}

