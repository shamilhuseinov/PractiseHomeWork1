using System;
using System.Globalization;
using Core.Entities;
using Core.Helpers;
using Data.Repository.Abstract;
using Data.Repository.Concrete;

namespace Presentation.Services
{
	public class TeacherService
	{
        public readonly GroupService _groupService;
        public readonly TeacherRepository _teacherRepository;
        private readonly GroupRepository _groupRepository;
        public TeacherService()
        {
            _groupService = new GroupService();
            _teacherRepository = new TeacherRepository();
            _groupRepository = new GroupRepository();
        }

        public void GetAll()
        {
            var teachers = _teacherRepository.GetAll();
            if (teachers.Count==0)
            {
                ConsoleHelper.WriteWithColor("There is no any teacher", ConsoleColor.Red);

            }
            foreach (var teacher in teachers)
            {
                ConsoleHelper.WriteWithColor($"Id: {teacher.Id} FullName: {teacher.Name} {teacher.Surname} Speciality: {teacher.Speciality}", ConsoleColor.Cyan);
                if (teacher.Groups.Count==0)
                {
                    ConsoleHelper.WriteWithColor("Teacher has no groups", ConsoleColor.Cyan);

                }
                foreach (var group in teacher.Groups)
                {
                    ConsoleHelper.WriteWithColor($"Id: {group.Id} Name: {group.Name}", ConsoleColor.Cyan);

                }
            }
        }

        public void GetTeacherByGroup()
        {
            if (_teacherRepository.GetAll().Count != 0)
            {
            GroupIdDesc: _groupService.GetAll();
                if (!(_groupRepository.GetAll().Count == 0))
                {
                    ConsoleHelper.WriteWithColor("Enter group id", ConsoleColor.Red);

                    int id;
                    bool issucceeded = int.TryParse(Console.ReadLine(), out id);

                    if (!issucceeded)
                    {
                        ConsoleHelper.WriteWithColor("Invalid input", ConsoleColor.Red);
                        goto GroupIdDesc;
                    }

                    var group = _groupRepository.Get(id);

                    if (group is null)
                    {
                        ConsoleHelper.WriteWithColor("There is no group in this id", ConsoleColor.Red);
                        goto GroupIdDesc;

                    }

                    ConsoleHelper.WriteWithColor($"{group.Teacher.Name} {group.Teacher.Surname}", ConsoleColor.Cyan);
                }
            }
            else
            {
                ConsoleHelper.WriteWithColor("there is no any teacher", ConsoleColor.Red);

            }
        }

        public void Create()
		{
			ConsoleHelper.WriteWithColor("Enter Name of the teacher",ConsoleColor.Cyan);
			string name = Console.ReadLine();

            ConsoleHelper.WriteWithColor("Enter Surname of the teacher", ConsoleColor.Cyan);
            string surname = Console.ReadLine();


            BirthDateDesc: ConsoleHelper.WriteWithColor("Enter Birth date of the teacher", ConsoleColor.Cyan);
			DateTime birthDate;
            bool issuccceed = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
            if (!issuccceed)
            {
                ConsoleHelper.WriteWithColor("Invalid input", ConsoleColor.Red);
                goto BirthDateDesc;
            }

            ConsoleHelper.WriteWithColor("Enter Teacher speciality", ConsoleColor.Cyan);
            string speciality = Console.ReadLine();

            var teacher = new Teacher
            {
                Name = name,
                Surname=surname,
                BirthDate=birthDate,
                Speciality=speciality,
                CreatedAt=DateTime.Now,

            };

            _teacherRepository.Add(teacher);
            string teacherBirthDate = teacher.BirthDate.ToString("dddd, dd MMMM yyyy");
            ConsoleHelper.WriteWithColor($"Name: {teacher.Name} Surname: {teacher.Surname} Speciality: {teacher.Speciality}, BirthDate: {teacherBirthDate}", ConsoleColor.Cyan);

        }

        public void Update()
        {
            IdDesc: GetAll();
            if (!(_teacherRepository.GetAll().Count==0))
            {
                ConsoleHelper.WriteWithColor("Enter id", ConsoleColor.Cyan);
                int id;
                bool issucceeded = int.TryParse(Console.ReadLine(), out id);
                if (!issucceeded)
                {
                    ConsoleHelper.WriteWithColor("Invalid input", ConsoleColor.Red);
                    goto IdDesc;
                }
                var teacher = _teacherRepository.Get(id);
                if (teacher is null)
                {
                    ConsoleHelper.WriteWithColor("There is no teacher in this id", ConsoleColor.Red);
                    goto IdDesc;
                }

                ConsoleHelper.WriteWithColor("Enter new name", ConsoleColor.Cyan);
                string name = Console.ReadLine();

                ConsoleHelper.WriteWithColor("Enter new surname", ConsoleColor.Cyan);
                string surname = Console.ReadLine();

            BirthDateDesc: ConsoleHelper.WriteWithColor("Enter the birthDate of the teacher", ConsoleColor.Cyan);
                DateTime birthDate;
                bool issuccceed = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
                if (!issuccceed)
                {
                    ConsoleHelper.WriteWithColor("Invalid input", ConsoleColor.Red);
                    goto BirthDateDesc;
                }

                ConsoleHelper.WriteWithColor("Enter new speciality", ConsoleColor.Cyan);
                string speciality = Console.ReadLine();

                teacher.Name = name;
                teacher.Surname = surname;
                teacher.BirthDate = birthDate;
                teacher.Speciality = speciality;

                _teacherRepository.Update(teacher);

                ConsoleHelper.WriteWithColor($"{teacher.Name} {teacher.Surname} is successfully updated", ConsoleColor.Green);
            }

        }

        public void Delete()
        {
            TeacherDesc: _teacherRepository.GetAll();

            if (_teacherRepository.GetAll().Count==0)
            {
                ConsoleHelper.WriteWithColor("There is no teacher", ConsoleColor.Red);
            }
            else
            {
                ConsoleHelper.WriteWithColor("Enter teacher id", ConsoleColor.Cyan);
                int id;
                bool issucceeded = int.TryParse(Console.ReadLine(), out id);
                if (!issucceeded)
                {
                    ConsoleHelper.WriteWithColor("Invalid input", ConsoleColor.Red);
                    goto TeacherDesc;
                }

                var teacher = _teacherRepository.Get(id);
                if (teacher is null)
                {
                    ConsoleHelper.WriteWithColor("no teacher in this id", ConsoleColor.Red);
                    goto TeacherDesc;
                }
                _teacherRepository.Delete(teacher);
                ConsoleHelper.WriteWithColor($"{teacher.Name} {teacher.Surname} is deleted successfully", ConsoleColor.Green);

            }
        }

        
    }
}

