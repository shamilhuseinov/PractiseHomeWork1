using System;
using Core.Entities;

namespace Data.Repository.Abstract
{
	public interface IRepository<T>where T:BaseEntity
	{
        List<T> GetAll();
        T Get(int id);
        void Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}

