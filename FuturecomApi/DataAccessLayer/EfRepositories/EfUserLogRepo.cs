using System;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;

namespace DataAccessLayer.EfRepositories
{
    public class EfUserLogRepo : GenericRepository<UserLog>, IUserLogDal
    {
        Context context = new Context();

        public List<UserLog> GetUserLog()
        {
            return context.UserLogs.ToList();
        }

        public void TInsert(string type, string userId)
        {
            try
            {
                UserLog userLog = new UserLog
                {
                    CreatedDate = DateTime.Now,
                    Type = type,
                    UserID = Guid.Parse(userId),


                };

                context.UserLogs.Add(userLog);
                context.SaveChanges();
            }
           
            catch(Exception e)
            {

                Console.WriteLine($"{e}");
            }
           
        }
    }
}

