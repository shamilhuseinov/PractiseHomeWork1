using System;
using Core.Entities;
using Core.Helpers;
using Data.Contexts;

namespace Data
{
	public static class DbInitializer
	{
		static int id;
		public static void SeedAdmins()
		{
			var admins = new List<Admin>
			{
				new Admin
				{
					Id=++id,
					Username="admin1",
					Password = PasswordHasher.Encyrpt("12345678"),
					CreatedBy="System"
				},

				new Admin
				{
					Id=++id,
					Username="admin2",
                    Password = PasswordHasher.Encyrpt("salam123"),
                    CreatedBy="System"
                }
            };

			DbContent.Admins.AddRange(admins);
		}
	}
}

