using System;
using Core.Helpers;
using System.Globalization;
using Core.Entities;
using Data.Repository.Concrete;

namespace Presentation.Services
{
	public class GroupService
	{
        private readonly GroupRepository _groupRepository;
        public readonly StudentRepository _studentRepository;
        private readonly TeacherRepository _teacherRepository;
        public GroupService()
        {
             _groupRepository = new GroupRepository();
            _studentRepository = new StudentRepository();
            _teacherRepository = new TeacherRepository();
        }
        public void GetAll()
        {
            var groups = _groupRepository.GetAll();
            if (groups.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no group", ConsoleColor.Red);

            }
            else
            {
                ConsoleHelper.WriteWithColor("---All groups---", ConsoleColor.Cyan);
                foreach (var group in groups)
                {
                    ConsoleHelper.WriteWithColor($"Id: {group.Id}\nName: {group.Name}\nMax size: {group.MaxSize}\nStart date: {group.StartDate}\nEnd Date: {group.EndDate}, CreatedBy: {group.CreatedBy}", ConsoleColor.Cyan);
                }
            }
        }

        public void GetGroupById(Admin admin)
        {
            var groups = _groupRepository.GetAll();
            if (groups.Count == 0)
            {
            YouSureDescription: ConsoleHelper.WriteWithColor("There is not any groups. Do you want to create a new group? --y or n--", ConsoleColor.DarkRed);
                char decision;
                bool isSucceed = char.TryParse(Console.ReadLine(), out decision);
                if (!isSucceed)
                {
                    ConsoleHelper.WriteWithColor("input is not in a correct format", ConsoleColor.Red);
                    goto YouSureDescription;
                }
                if (!(decision == 'y' || decision == 'n'))
                {
                    ConsoleHelper.WriteWithColor("Your choice is not correct", ConsoleColor.Red);
                    goto YouSureDescription;
                }
                if (decision == 'y')
                {
                    Create(admin);
                }

            }
            else
            {
                GetAll();
            EnterIdDescription: ConsoleHelper.WriteWithColor("---Enter Id---", ConsoleColor.Cyan);
                int id;
                bool issucceed = int.TryParse(Console.ReadLine(), out id);
                if (!issucceed)
                {
                    ConsoleHelper.WriteWithColor("input is not in a correct format", ConsoleColor.Red);
                    goto EnterIdDescription;
                }
                var group = _groupRepository.Get(id);
                if (group is null)
                {
                    ConsoleHelper.WriteWithColor("There is not any group in this id", ConsoleColor.Red);
                    goto EnterIdDescription;
                }
                ConsoleHelper.WriteWithColor($"Id: {group.Id}\nName: {group.Name}\nMax size: {group.MaxSize}\nStart date: {group.StartDate}\nEnd Date: {group.EndDate}\nCreated By:{group.CreatedBy}", ConsoleColor.Cyan);
            }
        }

        public void GetGroupByName()
        {
            GetAll();
            if (!(_groupRepository.GetAll().Count==0))
            {
            EnterNameDescription: ConsoleHelper.WriteWithColor("Enter group name", ConsoleColor.Cyan);
                string name = Console.ReadLine();
                var group = _groupRepository.GetByName(name);
                if (group is null)
                {
                    ConsoleHelper.WriteWithColor("There is not any group in this name", ConsoleColor.Red);
                    goto EnterNameDescription;
                }
                ConsoleHelper.WriteWithColor($"Id: {group.Id}\nName: {group.Name}\nMax size: {group.MaxSize}\nStart date: {group.StartDate}\nEnd Date: {group.EndDate}\nCreated by:{group.CreatedBy}", ConsoleColor.Cyan);
            }

        }

        public void Create(Admin admin)
		{
            if (_teacherRepository.GetAll().Count==0)
            {
                ConsoleHelper.WriteWithColor("You should create a teacher", ConsoleColor.Yellow);

            }
            else
            {
            GroupDesc: ConsoleHelper.WriteWithColor("Enter name", ConsoleColor.Cyan);
                string name = Console.ReadLine();
                var group = _groupRepository.GetByName(name);
                if (group is not null)
                {
                    ConsoleHelper.WriteWithColor("This name has already been used", ConsoleColor.Red);
                    goto GroupDesc;
                }
                int maxSize;
            MaxSizeDescription: ConsoleHelper.WriteWithColor("Enter max size of the group", ConsoleColor.Cyan);
                bool issucceed = int.TryParse(Console.ReadLine(), out maxSize);
                if (!issucceed)
                {
                    ConsoleHelper.WriteWithColor("Max Size size is not in correct format", ConsoleColor.Red);
                    goto MaxSizeDescription;
                }
                if (maxSize > 18)
                {
                    ConsoleHelper.WriteWithColor("max size must be equal or less than 18", ConsoleColor.Red);
                    goto MaxSizeDescription;
                }

            StartDateDescription: ConsoleHelper.WriteWithColor("enter the start date", ConsoleColor.Cyan);

                DateTime startDate;
                issucceed = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                if (!issucceed)
                {
                    ConsoleHelper.WriteWithColor("Start date is not in correct format", ConsoleColor.Red);
                    goto StartDateDescription;
                }

            EndDateDescription: ConsoleHelper.WriteWithColor("enter the end date", ConsoleColor.Cyan);

                DateTime endDate;
                issucceed = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
                if (!issucceed)
                {
                    ConsoleHelper.WriteWithColor("End date is not in correct format", ConsoleColor.Red);
                    goto EndDateDescription;
                }

                if (startDate > endDate)
                {
                    ConsoleHelper.WriteWithColor("End date must be bigger than the start date", ConsoleColor.Red);
                    goto EndDateDescription;
                }

                var Teachers = _teacherRepository.GetAll();
                foreach (var teacher in Teachers)
                {
                    ConsoleHelper.WriteWithColor($"Id: {teacher.Id} FullName: {teacher.Name} {teacher.Surname}", ConsoleColor.Cyan);

                }

                TeacherIdDesc: ConsoleHelper.WriteWithColor("enter teacher id", ConsoleColor.Cyan);
                int teacherId;
                issucceed = int.TryParse(Console.ReadLine(), out teacherId);
                if (!issucceed)
                {
                    ConsoleHelper.WriteWithColor("Invalid input", ConsoleColor.Red);
                    goto TeacherIdDesc;
                }
                var dbTeacher = _teacherRepository.Get(teacherId);
                if (dbTeacher is null)
                {
                    ConsoleHelper.WriteWithColor("no any teacher in this id", ConsoleColor.Red);
                    goto TeacherIdDesc;
                }

                group = new Group

                {
                    Name = name,
                    MaxSize = maxSize,
                    StartDate = startDate,
                    EndDate = endDate,
                    CreatedBy = admin.Username,
                    Teacher = dbTeacher
                };

                dbTeacher.Groups.Add(group);
                _groupRepository.Add(group);
                ConsoleHelper.WriteWithColor($"Group is created successfully with\nName: {group.Name}\nMax size: {group.MaxSize}\nStart date: {group.StartDate}\nEnd date: {group.EndDate}", ConsoleColor.Cyan);
            }
        }

        public void GetAllGroupsByTeacher()
        {
            var teachers = _teacherRepository.GetAll();
            foreach (var teacher in teachers)
            {
                ConsoleHelper.WriteWithColor($"Id: {teacher.Id} Name: {teacher.Name}", ConsoleColor.Cyan);

            }
            TeacherIdDesc: ConsoleHelper.WriteWithColor("Enter teacher id", ConsoleColor.Cyan);

            int id;
            bool issucceded = int.TryParse(Console.ReadLine(), out id);
            if (!issucceded)
            {
                ConsoleHelper.WriteWithColor("Invalid input", ConsoleColor.Red);
                goto TeacherIdDesc;
            }

            var dbTeacher = _teacherRepository.Get(id);
            if (dbTeacher is null)
            {
                ConsoleHelper.WriteWithColor("There is no teacher in this id", ConsoleColor.Red);
            }

            else
            {
                foreach (var group in dbTeacher.Groups)
                {
                    ConsoleHelper.WriteWithColor($"Id: {group.Id} Name: {group.Name}", ConsoleColor.Cyan);

                }
            }
        }

        public void Update(Admin admin)
        {
            if (_groupRepository.GetAll().Count==0)
            {
                ConsoleHelper.WriteWithColor("There is no group to update", ConsoleColor.Red);
                return;
            }

            GetAll();

        EnterGroupDescription: ConsoleHelper.WriteWithColor("--Enter\n1. Id or\n2. Name of the group--", ConsoleColor.Cyan);
            int number;
            bool issucceed = int.TryParse(Console.ReadLine(), out number);
            if (!issucceed)
            {
                ConsoleHelper.WriteWithColor("Input is not in a correct format", ConsoleColor.Red);
                goto EnterGroupDescription;
            }
            if (!(number == 1 || number == 2))
            {
                ConsoleHelper.WriteWithColor("Input is not correct", ConsoleColor.Red);
                goto EnterGroupDescription;
            }
            if (number == 1)
            {
            EnterIdDescription: ConsoleHelper.WriteWithColor("--Enter Id of the group--", ConsoleColor.Cyan);
                int id;
                issucceed = int.TryParse(Console.ReadLine(), out id);
                if (!issucceed)
                {
                    ConsoleHelper.WriteWithColor("Input is not in a correct format", ConsoleColor.Red);
                    goto EnterGroupDescription;
                }
                var group = _groupRepository.Get(id);
                if (group is null)
                {
                    ConsoleHelper.WriteWithColor("There is not any group in this Id", ConsoleColor.Red);
                    goto EnterIdDescription;
                }
                InternalUpdate(group, admin);

            }
            else
            {
            EnterNameDescription: ConsoleHelper.WriteWithColor("--Enter Name of the group--", ConsoleColor.Cyan);
                string name = Console.ReadLine();

                var group = _groupRepository.GetByName(name);

                if (group is null)
                {
                     ConsoleHelper.WriteWithColor("There is not any group in this name", ConsoleColor.Red);
                    goto EnterNameDescription;
                }

                InternalUpdate(group, admin);
            }
        }

        public void InternalUpdate(Group group, Admin admin)
        {
            ConsoleHelper.WriteWithColor("Enter new name", ConsoleColor.Cyan);
            string name = Console.ReadLine();

        MaxSizeDescription: ConsoleHelper.WriteWithColor("Enter new Max Size", ConsoleColor.Cyan);
            int maxSize;
            bool issucceed = int.TryParse(Console.ReadLine(), out maxSize);
            if (!issucceed)
            {
                ConsoleHelper.WriteWithColor("Input is not in a correct format", ConsoleColor.Red);
                goto MaxSizeDescription;
            }

        StartDateDescription: ConsoleHelper.WriteWithColor("Enter new start date", ConsoleColor.Cyan);
            DateTime startDate;
            issucceed = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
            if (!issucceed)
            {
                ConsoleHelper.WriteWithColor("Start date is not in correct format", ConsoleColor.Red);
                goto StartDateDescription;
            }

        EndDateDescription: ConsoleHelper.WriteWithColor("Enter new end date", ConsoleColor.Cyan);
            DateTime endDate;
            issucceed = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
            if (!issucceed)
            {
                ConsoleHelper.WriteWithColor("End date is not in correct format", ConsoleColor.Red);
                goto EndDateDescription;
            }

            group.Name = name;
            group.MaxSize = maxSize;
            group.StartDate = startDate;
            group.EndDate = endDate;
            group.ModifiedBy = admin.Username;

            _groupRepository.Update(group);
        }

        public void Delete()
        {
            var groups = _groupRepository.GetAll();
            if (groups.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no group to delete", ConsoleColor.Red);

            }

            else
            {
                ConsoleHelper.WriteWithColor("All groups", ConsoleColor.Cyan);
                GetAll();
            DeleteGroupDescription: ConsoleHelper.WriteWithColor("Silmek istediyiniz qrupun id si ni daxil edin", ConsoleColor.Cyan);

                int id;
                bool issucceed = int.TryParse(Console.ReadLine(), out id);
                if (!issucceed)
                {
                    ConsoleHelper.WriteWithColor("Invalid input", ConsoleColor.Red);
                    goto DeleteGroupDescription;
                }
                var group = _groupRepository.Get(id);
                if (group is null)
                {
                    ConsoleHelper.WriteWithColor("There is no group in this id", ConsoleColor.Red);
                    goto DeleteGroupDescription;
                }

                else
                {
                    foreach (var student in group.Students)
                    {
                        student.Group = null;
                        _studentRepository.Update(student);
                    }
                    _groupRepository.Delete(group);
                    ConsoleHelper.WriteWithColor("Group is successfully deleteed", ConsoleColor.Green);
                }
            }
        }
    }
}

