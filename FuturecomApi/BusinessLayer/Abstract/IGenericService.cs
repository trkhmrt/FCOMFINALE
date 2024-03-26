using System;
namespace BusinessLayer.Abstract
{
	public interface IGenericService<T> where T: class
	{
        void TInsert(T t);

        void TDelete(T t);

        void TUpdate(T t);

        T TGetByID(Guid id);

        List<T> TGetList();
    }
}

