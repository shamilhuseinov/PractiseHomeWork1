using System.Globalization;
using Core.Constants;
using Core.Helpers;
using Core.Entities;
using Data.Repository.Concrete;
using Presentation.Services;

namespace Presentation
{
    public static class Program
    {
        private readonly static GroupService _groupservice;
        static Program()
        {
            _groupservice = new GroupService();
        }
        static void Main()
        {
            ConsoleHelper.WriteWithColor("---Welcome---", ConsoleColor.Cyan);
            ConsoleHelper.WriteWithColor("--MAIN MENU--", ConsoleColor.Blue);


        MainMenuDescription: ConsoleHelper.WriteWithColor("Enter your option:\n1-Groups\n2-Students", ConsoleColor.Cyan);

            int choice;
            bool succeed = int.TryParse(Console.ReadLine(), out choice);
            if (!succeed)
            {
                ConsoleHelper.WriteWithColor("Invalid input", ConsoleColor.Red);
                goto MainMenuDescription;
            }

            switch (choice)
            {
                case 1:
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
                            if (!(number >= 0 && number <= 6))
                            {
                                ConsoleHelper.WriteWithColor("It does not exist", ConsoleColor.Red);
                            }
                            else
                            {
                                switch (number)
                                {
                                    case (int)GroupOptions.CreateGroup:
                                        _groupservice.Create();
                                        break;
                                    case (int)GroupOptions.UpdateGroup:
                                        _groupservice.Update();
                                        break;
                                    case (int)GroupOptions.DeleteGroup:
                                        _groupservice.Delete();
                                        break;
                                    case (int)GroupOptions.GetAllGroups:
                                        _groupservice.GetAll();
                                        break;
                                    case (int)GroupOptions.GetGroupById:
                                        _groupservice.GetGroupById();
                                        break;
                                    case (int)GroupOptions.GetGroupByName:
                                        _groupservice.GetGroupByName();
                                        break;
                                    case (int)GroupOptions.Exit:
                                        if (_groupservice.Exit() == true)
                                        {
                                            return;
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    break;
                case 2:
                    break;
                default:
                    ConsoleHelper.WriteWithColor("You should choose another option", ConsoleColor.Red);
                    goto MainMenuDescription;
                    break;
            }
        }
    }
}
