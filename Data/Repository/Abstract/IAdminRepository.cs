using System;
using Core.Entities;

namespace Data.Repository.Abstract
{
	public interface IAdminRepository
	{
		Admin GetByUserNameAndPassword(string username, string password);
	}
}

