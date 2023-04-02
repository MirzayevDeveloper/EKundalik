// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EKundalik.Brokers.Storages;
using EKundalik.Models.Teachers;
using EKundalik.Services.Teachers;
using Newtonsoft.Json;

namespace EKundalik.ConsoleLayer
{
    public class TeacherLayer
    {
        private readonly ITeacherService TeacherService;

        public TeacherLayer(IStorageBroker storageBroker) =>
            this.TeacherService = new TeacherService(storageBroker);

        public async Task TeacherCase()
        {
            bool isActive = true;

            while (isActive)
            {
                Console.Clear();
                int choice = General.PrintCrudOptions(nameof(Teacher));
                Console.Beep();

                switch (choice)
                {
                    case 1:
                        {
                            Teacher maybeTeacher =
                                await AddTeacherMenu();

                            await this.TeacherService
                                .AddTeacherAsync(maybeTeacher);
                        }
                        break;
                    case 2:
                        {
                            await SelectTeacher();
                        }
                        break;
                    case 3:
                        {
                            Teacher maybeTeacher =
                                await UpdateTeacher();

                            Teacher storageTeacher = await this.TeacherService
                                .ModifyTeacherAsync(maybeTeacher);

                            General.PrintObjectProperties(storageTeacher);
                        }
                        break;
                    case 4:
                        {
                            Teacher maybeTeacher = DeleteTeacher();

                            await this.TeacherService
                                .RemoveTeacherByIdAsync(maybeTeacher.Id);
                        }
                        break;
                    case 5:
                        {
                            IQueryable<Teacher> Teachers =
                                this.TeacherService.RetrieveAllTeachers();

                            General.SelectAll(Teachers);
                        }
                        break;
                    case 6:
                        AddTeacher();
                        break;
                    case 7:
                        isActive = false;
                        break;
                }
                General.Pause();
            }
        }

        private Teacher DeleteTeacher()
        {
            Teacher Teacher = SelectTeacher().Result ?? new();

            return Teacher;
        }

        private async ValueTask<Teacher> UpdateTeacher()
        {
            bool isActive = true;
            Teacher Teacher = SelectTeacher().Result;

            if (Teacher != null)
            {
                await WriteToFile(Teacher);
            }

            while (isActive && Teacher != null)
            {
                if (ReadFromFile().Id != default)
                {
                    Console.Clear();
                    General.PrintObjectProperties(Teacher);

                    Console.Write("1.Username\n" +
                                    "2.FullName\n" +
                                    "3.BirthDate\n" +
                                    "4.Exit\n" +
                                    "Which property do you want to change: ");

                    string choose = Console.ReadLine();
                    int choice;
                    int.TryParse(choose, out choice);

                    Console.Beep();

                    switch (choice)
                    {
                        case 1:
                            {
                                Console.Write("Create new username: ");
                                string user = Console.ReadLine();

                                Teacher.UserName = user;
                            }
                            break;
                        case 2:
                            {
                                Console.Write("Enter new Full name: ");
                                string name = Console.ReadLine();

                                Teacher.FullName = name;
                            }
                            break;
                        case 3:
                            {
                                Console.Write("Enter a new birth date: ");
                                string birth = Console.ReadLine();
                                DateTime birthDate;
                                DateTime.TryParse(birth, out birthDate);

                                Teacher.BirthDate = birthDate;
                            }
                            break;
                        case 4:
                            isActive = false;
                            break;
                    }
                    await WriteToFile(Teacher);

                    if (choice != 4) General.Sleep();
                }
            }
            Console.Clear();
            Teacher updatedTeacher = ReadFromFile();

            File.WriteAllText(
                "../../../ConsoleLayer/data.json", "");

            return updatedTeacher;
        }

        private async ValueTask WriteToFile(Teacher Teacher)
        {
            File.WriteAllText("../../../ConsoleLayer/data.json",
                JsonConvert.SerializeObject(Teacher, Formatting.Indented));
        }

        private Teacher ReadFromFile()
        {
            return JsonConvert.DeserializeObject<Teacher>(
                File.ReadAllText("../../../ConsoleLayer/data.json"));
        }

        private async ValueTask<Teacher> SelectTeacher()
        {
            Console.Write("Enter username: ");
            string user = Console.ReadLine();

            Teacher maybeTeacher =
                await this.TeacherService
                .RetrieveTeacherByUserName(user);

            General.PrintObjectProperties(maybeTeacher);

            return maybeTeacher;
        }

        private async void AddTeacher()
        {
            Console.Write("Nechta Teacher Objectni Tablega kiritmoqchisiz: ");
            string choice = Console.ReadLine();
            int count;
            int.TryParse(choice, out count);

            if (count is 0) return;

            List<Teacher> list = new List<Teacher>();
            Teacher maybeTeacher = new();

            for (int i = 0; i < count; i++)
            {
                maybeTeacher = await this.TeacherService.AddTeacherAsync(
                    General.CreateObjectFiller<Teacher>().Create());
                list.Add(maybeTeacher);
            }

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{maybeTeacher.Id}\n{maybeTeacher.FullName}");
            }
        }

        private async ValueTask<Teacher> AddTeacherMenu()
        {
            Console.Write("create your username: ");
            string username = Console.ReadLine();

            Console.Write("enter Full name: ");
            string fullName = Console.ReadLine();

            Console.Write("enter BirthDate [month:day:year]: ");
            string dateTime = Console.ReadLine();
            DateTime birthDate;
            DateTime.TryParse(dateTime, out birthDate);

            Console.Write("Gender[m/f]: ");
            string gender = Console.ReadLine();
            bool genderBool = gender == "m" ? true : false;

            var Teacher = new Teacher()
            {
                Id = Guid.NewGuid(),
                FullName = fullName,
                UserName = username.ToLower(),
                BirthDate = birthDate,
                Gender = genderBool
            };

            return Teacher;
        }
    }
}
