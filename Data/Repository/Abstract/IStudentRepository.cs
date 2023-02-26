using System;
using Core.Entities;

namespace Data.Repository.Abstract
{
	public interface IStudentRepository:IRepository<Student>
	{
		bool IsDublicateEmail(string email);
	}
}

