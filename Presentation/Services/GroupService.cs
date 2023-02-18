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
        public GroupService()
        {
             _groupRepository = new GroupRepository();
        }
        public void GetAll()
        {
            var groups = _groupRepository.GetAll();
            if (groups.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no group to get", ConsoleColor.Red);

            }
            else
            {
                ConsoleHelper.WriteWithColor("---All groups---", ConsoleColor.Cyan);
                foreach (var group in groups)
                {
                    ConsoleHelper.WriteWithColor($"Id: {group.Id}\nName: {group.Name}\nMax size: {group.MaxSize}\nStart date: {group.StartDate}\nEnd Date: {group.EndDate}", ConsoleColor.Cyan);
                }
            }
        }

        public void GetGroupById()
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
                    Create();
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
                ConsoleHelper.WriteWithColor($"Id: {group.Id}\nName: {group.Name}\nMax size: {group.MaxSize}\nStart date: {group.StartDate}\nEnd Date: {group.EndDate}", ConsoleColor.Cyan);
            }
        }

        public void GetGroupByName()
        {
            GetAll();
            EnterNameDescription: ConsoleHelper.WriteWithColor("Enter group name", ConsoleColor.Cyan);
            string name = Console.ReadLine();
            var group = _groupRepository.GetByName(name);
            if (group is null)
            {
                ConsoleHelper.WriteWithColor("There is not any group in this name", ConsoleColor.Red);
                goto EnterNameDescription;
            }
            ConsoleHelper.WriteWithColor($"Id: {group.Id}\nName: {group.Name}\nMax size: {group.MaxSize}\nStart date: {group.StartDate}\nEnd Date: {group.EndDate}", ConsoleColor.Cyan);

        }

        public void Create()
		{
            ConsoleHelper.WriteWithColor("Enter name", ConsoleColor.Cyan);
            string name = Console.ReadLine();
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

            var group = new Group
            {
                Name = name,
                MaxSize = maxSize,
                StartDate = startDate,
                EndDate = endDate
            };

            _groupRepository.Add(group);
            ConsoleHelper.WriteWithColor($"Group is created successfully with\nName: {group.Name}\nMax size: {group.MaxSize}\nStart date: {group.StartDate}\nEnd date: {group.EndDate}", ConsoleColor.Cyan);
        }

        public void Update()
        {
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
                InternalUpdate(group);

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

                InternalUpdate(group);
            }
        }

        public void InternalUpdate(Group group)
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

                _groupRepository.Delete(group);
                ConsoleHelper.WriteWithColor("Group is successfully deleteed", ConsoleColor.Green);
            }
        }

        public bool Exit()
        {
        AreYouSureDescription: ConsoleHelper.WriteWithColor("Are you sure? --y or n--", ConsoleColor.DarkRed);
            char decision;

            bool issucceed = char.TryParse(Console.ReadLine(), out decision);
            if (!issucceed)
            {
                ConsoleHelper.WriteWithColor("input is not in a correct format", ConsoleColor.Red);
                goto AreYouSureDescription;
            }
            if (!(decision == 'y' || decision == 'n'))
            {
                ConsoleHelper.WriteWithColor("Your choice is not correct", ConsoleColor.Red);
                goto AreYouSureDescription;
            }
            if (decision=='y')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

