using System.Globalization;
using Core.Constants;
using Core.Helpers;
using Core.Entities;
using Data.Repository.Concrete;

namespace Presentation
{
    public static class Program
    {
        static void Main()
        {
            GroupRepository _groupRepository = new GroupRepository();

            ConsoleHelper.WriteWithColor("---Welcome---", ConsoleColor.Cyan);


            while (true)
            {
                ConsoleHelper.WriteWithColor("1-Create Group", ConsoleColor.Yellow);

                ConsoleHelper.WriteWithColor("2-Update Group", ConsoleColor.Yellow);

                ConsoleHelper.WriteWithColor("3-Delete Group", ConsoleColor.Yellow);

                ConsoleHelper.WriteWithColor("4-Get All Groups", ConsoleColor.Yellow);

                ConsoleHelper.WriteWithColor("5-Get Group By Id", ConsoleColor.Yellow);

                ConsoleHelper.WriteWithColor("6-Get Group By Name", ConsoleColor.Yellow);

                ConsoleHelper.WriteWithColor("0-Exit", ConsoleColor.Yellow);

                ConsoleHelper.WriteWithColor("---Select Option---", ConsoleColor.Cyan);

                int number;
                bool issucceed = int.TryParse(Console.ReadLine(), out number);
                if (!issucceed)
                {
                    ConsoleHelper.WriteWithColor("Invalid input", ConsoleColor.Red);
                }
                else
                {
                    if (!(number>=0&&number<=6))
                    {
                        ConsoleHelper.WriteWithColor("It does not exist", ConsoleColor.Red);
                    }
                    else
                    {
                        switch (number)
                        {
                            case (int)GroupOptions.CreateGroup:
                                ConsoleHelper.WriteWithColor("Enter name", ConsoleColor.Cyan);
                                string name = Console.ReadLine();
                                int maxSize;
                                MaxSizeDescription: ConsoleHelper.WriteWithColor("Enter max size of the group", ConsoleColor.Cyan);
                                issucceed = int.TryParse(Console.ReadLine(), out maxSize);
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

                                if (startDate>endDate)
                                {
                                    ConsoleHelper.WriteWithColor("End date must be bigger than the start date", ConsoleColor.Red);
                                    goto EndDateDescription;
                                }

                                var group = new Group
                                {
                                    Name = name,
                                    MaxSize=maxSize,
                                    StartDate=startDate,
                                    EndDate=endDate
                                };

                                _groupRepository.Add(group);
                                ConsoleHelper.WriteWithColor($"Group is created successfully with\nName: {group.Name}\nMax size: {group.MaxSize}\nStart date: {group.StartDate}\nEnd date: {group.EndDate}", ConsoleColor.Cyan);
                                break;

                            case (int)GroupOptions.UpdateGroup:
                                break;
                            case (int)GroupOptions.DeleteGroup:
                                var groupss = _groupRepository.GetAll();
                                ConsoleHelper.WriteWithColor("---All Groups---", ConsoleColor.Cyan);
                                foreach (var group_ in groupss)
                                {
                                    ConsoleHelper.WriteWithColor($"Id: {group_.Id}\nName: {group_.Name}\nMax size: {group_.MaxSize}\nStart date: {group_.StartDate}\nEnd Date: {group_.EndDate}", ConsoleColor.Cyan);
                                }

                                IdDescription: ConsoleHelper.WriteWithColor("---Enter Id---", ConsoleColor.Cyan);

                                int id;
                                issucceed = int.TryParse(Console.ReadLine(), out id);
                                if (!issucceed)
                                {
                                    ConsoleHelper.WriteWithColor("Id is not in a correct format", ConsoleColor.Red);
                                    goto IdDescription;
                                }
                                var dbGroup = _groupRepository.Get(id);
                                if (dbGroup is null)
                                {
                                    ConsoleHelper.WriteWithColor("There is no group with this Id", ConsoleColor.Red);
                                }
                                else
                                {
                                    _groupRepository.Delete(dbGroup);
                                    ConsoleHelper.WriteWithColor("Group is successfully deleted", ConsoleColor.Green);
                                }
                                break;
                            case (int)GroupOptions.GetAllGroups:
                                var groups = _groupRepository.GetAll();
                                ConsoleHelper.WriteWithColor("---All Groups---", ConsoleColor.Cyan);
                                foreach (var group_ in groups)
                                {
                                    ConsoleHelper.WriteWithColor($"Id: {group_.Id}\nName: {group_.Name}\nMax size: {group_.MaxSize}\nStart date: {group_.StartDate}\nEnd Date: {group_.EndDate}",ConsoleColor.Cyan);
                                }
                                break;
                            case (int)GroupOptions.GetGroupById:
                                break;
                            case (int)GroupOptions.GetGroupByName:
                                break;
                            case (int)GroupOptions.Exit:
                                return;
                        }
                    }                
                }
            }
        }
    }
}
