using System;
using System.Globalization;
using Core.Entities;
using Core.Extensions;
using Core.Helpers;
using Data.Repository.Concrete;

namespace Presentation.Services
{
	public class StudentService
	{
		private readonly GroupRepository _groupRepository;
		private readonly GroupService _groupService;
		private readonly StudentRepository _studentRepository;
		public StudentService()
		{
			_groupRepository = new GroupRepository();
			_groupService = new GroupService();
			_studentRepository = new StudentRepository();

        }
		int id;

		public void GetAll()
		{
            if (!(_studentRepository.GetAll().Count==0))
            {
                var Students = _studentRepository.GetAll();

                ConsoleHelper.WriteWithColor("--All students--", ConsoleColor.DarkRed);

                foreach (var student in Students)
                {
                    ConsoleHelper.WriteWithColor($"Id: {student.Id}, Name: {student.Name}, Surname: {student.Surname}, BirthDate: {student.BirthDate}, Email: {student.Email}, Group: {student.Group?.Name}, CreatedBy: {student.CreatedBy}", ConsoleColor.Cyan);
                }
            }
            else
            {
                ConsoleHelper.WriteWithColor("there is no student", ConsoleColor.Red);
                return;
            }
        }

		public void GetAllByGroup()
		{

            if (_studentRepository.GetAll().Count!=0)
            {
            GroupDesc: _groupService.GetAll();

                ConsoleHelper.WriteWithColor("Enter group id", ConsoleColor.Cyan);

                int GroupId;
                bool issucceeded = int.TryParse(Console.ReadLine(), out GroupId);
                if (!issucceeded)
                {
                    ConsoleHelper.WriteWithColor("invalid input", ConsoleColor.Red);
                    goto GroupDesc;
                }

                var group = _groupRepository.Get(GroupId);
                if (group is null)
                {
                    ConsoleHelper.WriteWithColor("There is no group in this id", ConsoleColor.Red);
                }

                if (group.Students.Count == 0)
                {
                    ConsoleHelper.WriteWithColor("There is no student in this group", ConsoleColor.Red);

                }
                else
                {
                    foreach (var student in group.Students)
                    {
                        ConsoleHelper.WriteWithColor($"Id: {student.Id}, Name: {student.Name}, Surname: {student.Surname}, BirthDate: {student.BirthDate}, Email: {student.Email}", ConsoleColor.Cyan);

                    }
                }
            }
            else
            {
                ConsoleHelper.WriteWithColor("There is no student", ConsoleColor.Red);

            }
        }

        public void CreateStudent(Admin admin)
        {
            if (_groupRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("yuou should create a group firts", ConsoleColor.Red);
                return;
            }
            id++;
            ConsoleHelper.WriteWithColor("Enter the name of the student", ConsoleColor.Cyan);
            string name = Console.ReadLine();

            ConsoleHelper.WriteWithColor("Enter the surname of the student", ConsoleColor.Cyan);
            string surname = Console.ReadLine();

        BirthDateDesc: ConsoleHelper.WriteWithColor("Enter the birthDate of the student", ConsoleColor.Cyan);
            DateTime birthDate;
            bool issuccceed = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
            if (!issuccceed)
            {
                ConsoleHelper.WriteWithColor("Invalid input", ConsoleColor.Red);
                goto BirthDateDesc;
            }


        EmailDesc: ConsoleHelper.WriteWithColor("Enter the email of the student", ConsoleColor.Cyan);
            string email = Console.ReadLine();
            if (!email.IsEmail())
            {
                ConsoleHelper.WriteWithColor("Invalid Input", ConsoleColor.Red);
                goto EmailDesc;
            }

            if (_studentRepository.IsDublicateEmail(email))
            {
                ConsoleHelper.WriteWithColor("This email is already used", ConsoleColor.Red);
                goto EmailDesc;
            }

            _groupService.GetAll();

        GroupDesc:
            ConsoleHelper.WriteWithColor("Enter Group id", ConsoleColor.Cyan);

            int GroupId;
            issuccceed = int.TryParse(Console.ReadLine(), out GroupId);
            if (!issuccceed)
            {
                ConsoleHelper.WriteWithColor("Invalid Input", ConsoleColor.Red);
            }
            var group = _groupRepository.Get(GroupId);

            if (group is null)
            {
                ConsoleHelper.WriteWithColor("group does not exist in this id", ConsoleColor.Cyan);
                goto GroupDesc;
            }

            if (group.MaxSize <= group.Students.Count)
            {
                ConsoleHelper.WriteWithColor("this group is full, choose another group", ConsoleColor.Red);
                goto GroupDesc;
            }
            var student = new Student
            {
                Id = id,
                Name = name,
                Surname = surname,
                BirthDate = birthDate,
                Email = email,
                Group = group,
                GroupId = group.Id,
                CreatedBy=admin.Username
            };
            group.Students.Add(student);
            _studentRepository.Add(student);
            ConsoleHelper.WriteWithColor($"{student.Name}, {student.Surname} is successfully added", ConsoleColor.Green);
        }

        public void Update(Admin admin)
        {
        StudentDesc: GetAll();

            if (!(_studentRepository.GetAll().Count==0))
            {
                int id;
                ConsoleHelper.WriteWithColor("Enter student id", ConsoleColor.Cyan);
                bool issucceeded = int.TryParse(Console.ReadLine(), out id);
                if (!issucceeded)
                {
                    ConsoleHelper.WriteWithColor("Invalid input", ConsoleColor.Red);
                    goto StudentDesc;
                }

                var student = _studentRepository.Get(id);
                if (student is null)
                {
                    ConsoleHelper.WriteWithColor("There is no student in this id", ConsoleColor.Red);
                    goto StudentDesc;
                }

                ConsoleHelper.WriteWithColor("Enter new name", ConsoleColor.Cyan);
                string name = Console.ReadLine();

                ConsoleHelper.WriteWithColor("Enter new surname", ConsoleColor.Cyan);
                string surname = Console.ReadLine();

            BirthDateDesc: ConsoleHelper.WriteWithColor("Enter the birthDate of the student", ConsoleColor.Cyan);
                DateTime birthDate;
                bool issuccceed = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
                if (!issuccceed)
                {
                    ConsoleHelper.WriteWithColor("Invalid input", ConsoleColor.Red);
                    goto BirthDateDesc;
                }

                _groupService.GetAll();

                ConsoleHelper.WriteWithColor("Enter new group id", ConsoleColor.Cyan);

            IdDesc: int groupId;
                issuccceed = int.TryParse(Console.ReadLine(), out groupId);
                if (!issuccceed)
                {
                    ConsoleHelper.WriteWithColor("Invalid input", ConsoleColor.Red);
                    goto IdDesc;
                }

                var group = _groupRepository.Get(groupId);
                if (group == null)
                {
                    ConsoleHelper.WriteWithColor("There is no any group in this id", ConsoleColor.Red);
                    goto IdDesc;
                }
                student.Name = name;
                student.Surname = surname;
                student.BirthDate = birthDate;
                student.Group = group;
                student.GroupId = groupId;
                student.ModifiedBy = admin.Username;

                _studentRepository.Update(student);

                ConsoleHelper.WriteWithColor($"{student.Name} {student.Surname}, Group: {student.Group.Name}  is successfully updated", ConsoleColor.Cyan);
            }
        }

        public void Delete()
		{
			GetAll();
            if (!(_studentRepository.GetAll().Count==0))
            {
            IdDesc: ConsoleHelper.WriteWithColor("Enter student id", ConsoleColor.Cyan);
                int id;
                bool issucceeded = int.TryParse(Console.ReadLine(), out id);
                if (!issucceeded)
                {
                    ConsoleHelper.WriteWithColor("Invalid input", ConsoleColor.Red);
                    goto IdDesc;
                }

                var student = _studentRepository.Get(id);
                if (student is null)
                {
                    ConsoleHelper.WriteWithColor("There is no student in this id", ConsoleColor.Red);

                }
                _studentRepository.Delete(student);
                ConsoleHelper.WriteWithColor($"{student.Name} {student.Surname} is deleted successfully", ConsoleColor.Green);
            }

        }
    }
}

