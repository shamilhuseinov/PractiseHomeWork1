using System.Globalization;
using Core.Constants;
using Core.Helpers;
using System.Text;
using Core.Entities;
using Data.Repository.Concrete;
using Presentation.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Data;
using Data.Contexts;

namespace Presentation
{
    public static class Program
    {
        private readonly static GroupService _groupservice;
        private readonly static StudentService _studentservice;
        private readonly static TeacherService _teacherService;
        private readonly static AdminService _adminService;

        static Program()
        {
            DbInitializer.SeedAdmins();

            _adminService = new AdminService();
            _groupservice = new GroupService();
            _studentservice = new StudentService();
            _teacherService = new TeacherService();
        }
        static void Main()
        {
            Console.OutputEncoding =Encoding.UTF8;
            ConsoleHelper.WriteWithColor("---Welcome---", ConsoleColor.Cyan);

        AuthorizeDesc: var admin = _adminService.Authorize();

                MainMenuDesc: while (true)
                {
                    ConsoleHelper.WriteWithColor("--MAIN MENU--", ConsoleColor.Blue);

                    ConsoleHelper.WriteWithColor("1-Groups", ConsoleColor.Yellow);
                    ConsoleHelper.WriteWithColor("2-Students", ConsoleColor.Yellow);
                    ConsoleHelper.WriteWithColor("3-Teachers", ConsoleColor.Yellow);

                    ConsoleHelper.WriteWithColor("0-Logout", ConsoleColor.Yellow);


                MainMenuDescription: ConsoleHelper.WriteWithColor("Enter your option", ConsoleColor.Cyan);

                    int choice;
                    bool succeed = int.TryParse(Console.ReadLine(), out choice);
                    if (!succeed)
                    {
                        ConsoleHelper.WriteWithColor("Invalid input", ConsoleColor.Red);
                        goto MainMenuDescription;
                    }

                    switch (choice)
                    {
                        case (int)MainMenuOptions.Logout:
                            goto AuthorizeDesc;
                        case (int)MainMenuOptions.Groups:
                            while (true)
                            {
                                ConsoleHelper.WriteWithColor("1-Create Group", ConsoleColor.Yellow);

                                ConsoleHelper.WriteWithColor("2-Update Group", ConsoleColor.Yellow);

                                ConsoleHelper.WriteWithColor("3-Delete Group", ConsoleColor.Yellow);

                                ConsoleHelper.WriteWithColor("4-Get All Groups", ConsoleColor.Yellow);

                                ConsoleHelper.WriteWithColor("5-Get Group By Id", ConsoleColor.Yellow);

                                ConsoleHelper.WriteWithColor("6-Get Group By Name", ConsoleColor.Yellow);

                                ConsoleHelper.WriteWithColor("0-Goto Main Menu", ConsoleColor.Yellow);

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
                                                _groupservice.Create(admin);
                                                break;
                                            case (int)GroupOptions.UpdateGroup:
                                                _groupservice.Update(admin);
                                                break;
                                            case (int)GroupOptions.DeleteGroup:
                                                _groupservice.Delete();
                                                break;
                                            case (int)GroupOptions.GetAllGroups:
                                                _groupservice.GetAll();
                                                break;
                                            case (int)GroupOptions.GetGroupById:
                                                _groupservice.GetGroupById(admin);
                                                break;
                                            case (int)GroupOptions.GetGroupByName:
                                                _groupservice.GetGroupByName();
                                                break;
                                            case (int)GroupOptions.GetAllGroupsByTeacher:
                                                _groupservice.GetGroupByName();
                                                break;
                                        case (int)GroupOptions.GotoMainMenu:
                                                goto MainMenuDesc;
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        case (int)MainMenuOptions.Students:
                            while (true)
                            {
                                ConsoleHelper.WriteWithColor("1-Create Student", ConsoleColor.Yellow);

                                ConsoleHelper.WriteWithColor("2-Update Student", ConsoleColor.Yellow);

                                ConsoleHelper.WriteWithColor("3-Delete Student", ConsoleColor.Yellow);

                                ConsoleHelper.WriteWithColor("4-Get All Students", ConsoleColor.Yellow);

                                ConsoleHelper.WriteWithColor("5-Get All Students by Group", ConsoleColor.Yellow);

                                ConsoleHelper.WriteWithColor("0-Goto MainMenu", ConsoleColor.Yellow);

                            StudentOptionDesc: ConsoleHelper.WriteWithColor("---Select Option---", ConsoleColor.Cyan);


                                int option;
                                bool issucceed = int.TryParse(Console.ReadLine(), out option);
                                if (!issucceed)
                                {
                                    ConsoleHelper.WriteWithColor("Invalid input", ConsoleColor.Red);
                                    goto StudentOptionDesc;
                                }

                                if (!(option >= 0 && option <= 5))
                                {
                                    ConsoleHelper.WriteWithColor("It does not exist", ConsoleColor.Red);
                                    goto StudentOptionDesc;
                                }

                                switch (option)
                                {
                                    case (int)StudentOptions.CreateStudent:
                                        _studentservice.CreateStudent(admin);
                                        break;
                                    case (int)StudentOptions.UpdateStudent:
                                        _studentservice.Update(admin);
                                        break;
                                    case (int)StudentOptions.DeleteStudent:
                                        _studentservice.Delete();
                                        break;
                                    case (int)StudentOptions.GetAllStudents:
                                        _studentservice.GetAll();
                                        break;
                                    case (int)StudentOptions.GetAllStudentsBygroup:
                                        _studentservice.GetAllByGroup();
                                        break;
                                    case (int)StudentOptions.GotoMainMenu:
                                        goto MainMenuDesc;
                                        break;

                                    default:
                                        break;
                                }

                            }
                            break;
                        case (int)MainMenuOptions.Teachers:
                            while (true)
                            {
                            TeacherDesc: ConsoleHelper.WriteWithColor("1-Create Teacher", ConsoleColor.Yellow);

                                ConsoleHelper.WriteWithColor("2-Update Teacher", ConsoleColor.Yellow);

                                ConsoleHelper.WriteWithColor("3-Delete Teacher", ConsoleColor.Yellow);

                                ConsoleHelper.WriteWithColor("4-Get All Teachers", ConsoleColor.Yellow);

                                ConsoleHelper.WriteWithColor("5-Get Teacher By Group", ConsoleColor.Yellow);

                                ConsoleHelper.WriteWithColor("0-Goto Main Menu", ConsoleColor.Yellow);

                                ConsoleHelper.WriteWithColor("---Select Option---", ConsoleColor.Cyan);

                                int number;
                                bool issucceed = int.TryParse(Console.ReadLine(), out number);
                                if (!issucceed)
                                {
                                    ConsoleHelper.WriteWithColor("Invalid input", ConsoleColor.Red);
                                }
                                else
                                {
                                    if (!(number >= 0 && number <= 5))
                                    {
                                        ConsoleHelper.WriteWithColor("It does not exist", ConsoleColor.Red);
                                    }
                                    else
                                    {
                                        switch (number)
                                        {
                                            case (int)TeacherOptions.CreateTeacher:
                                            _teacherService.Create();
                                                break;
                                            case (int)TeacherOptions.UpdateTeacher:
                                            _teacherService.Update();
                                                break;
                                            case (int)TeacherOptions.DeleteTeacher:
                                            _teacherService.Delete();
                                                break;
                                            case (int)TeacherOptions.GetAllTeachers:
                                            _teacherService.GetAll();
                                                break;
                                            case (int)TeacherOptions.GetAllTeachersByGroup:
                                            _teacherService.GetTeacherByGroup();
                                                break;
                                            case (int)TeacherOptions.GotoMainMenu:
                                                goto MainMenuDesc;
                                        }
                                    }
                                }
                            }
                        default:
                            ConsoleHelper.WriteWithColor("Choose Another Option", ConsoleColor.Red);
                            goto MainMenuDesc;
                    }
            }
        }
    }
}
