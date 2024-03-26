using System;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
	public class ErrorManager:IErrorService
	{
		private readonly IErrorDal _errorDal;

		public ErrorManager(IErrorDal errorDal)
		{
			_errorDal = errorDal;
		}

        public void TDelete(Error t)
        {
            _errorDal.Delete(t);
        }

        public Error TGetByID(Guid id)
        {
            return _errorDal.GetByID(id);
        }

        public List<Error> TGetList()
        {
           return  _errorDal.GetList();
        }

        public void TInsert(Error t)
        {
            _errorDal.Insert(t);
        }

        public void TUpdate(Error t)
        {
            _errorDal.Update(t);
        }
    }
}

