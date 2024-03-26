using System;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract.IGenericLogServices
{
    public interface IUserLogService : IGenericService<UserLog>
    {
        void TInsert(string type,string userId);
    }
}

