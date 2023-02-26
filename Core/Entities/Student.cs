﻿using System;
namespace Core.Entities
{
	public class Student:BaseEntity
	{
		public string Name { get; set; }

		public string Surname { get; set; }

		public DateTime BirthDate { get; set; }

		public string Email { get; set; }

		public Group Group { get; set; }

		public int GroupId { get; set; }
	}
}

