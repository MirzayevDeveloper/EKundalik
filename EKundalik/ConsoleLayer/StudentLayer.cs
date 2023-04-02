// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EKundalik.Brokers.Storages;
using EKundalik.Models.Students;
using EKundalik.Services.Students;
using Newtonsoft.Json;

namespace EKundalik.ConsoleLayer
{
    public class StudentLayer
    {
        private readonly IStudentService studentService;

        public StudentLayer(IStorageBroker storageBroker) =>
            this.studentService = new StudentService(storageBroker);

        public async Task StudentCase()
        {
            bool isActive = true;

            while (isActive)
            {
                Console.Clear();
                int choice = General.PrintCrudOptions(nameof(Student));
                Console.Beep();

                switch (choice)
                {
                    case 1:
                        {
                            Student maybeStudent =
                                await AddStudentMenu();

                            await this.studentService
                                .AddStudentAsync(maybeStudent);
                        }
                        break;
                    case 2:
                        {
                            await SelectStudent();
                        }
                        break;
                    case 3:
                        {
                            Student maybeStudent =
                                await UpdateStudent();

                            Student storageStudent = await this.studentService
                                .ModifyStudentAsync(maybeStudent);

                            General.PrintObjectProperties(storageStudent);
                        }
                        break;
                    case 4:
                        {
                            Student maybeStudent = DeleteStudent();

                            await this.studentService
                                .RemoveStudentByIdAsync(maybeStudent.Id);
                        }
                        break;
                    case 5:
                        {
                            IQueryable<Student> students =
                                this.studentService.RetrieveAllStudents();

                            General.SelectAll(students);
                        }
                        break;
                    case 6:
                        AddStudent();
                        break;
                    case 7:
                        isActive = false;
                        break;
                }
                General.Pause();
            }
        }

        private Student DeleteStudent()
        {
            Student student = SelectStudent().Result ?? new();

            return student;
        }

        private async ValueTask<Student> UpdateStudent()
        {
            bool isActive = true;
            Student student = SelectStudent().Result;

            if (student != null)
            {
                await WriteToFile(student);
            }

            while (isActive && student != null)
            {
                if (ReadFromFile().Id != default)
                {
                    Console.Clear();
                    General.PrintObjectProperties(student);

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

                                student.UserName = user;
                            }
                            break;
                        case 2:
                            {
                                Console.Write("Enter new Full name: ");
                                string name = Console.ReadLine();

                                student.FullName = name;
                            }
                            break;
                        case 3:
                            {
                                Console.Write("Enter a new birth date: ");
                                string birth = Console.ReadLine();
                                DateTime birthDate;
                                DateTime.TryParse(birth, out birthDate);

                                student.BirthDate = birthDate;
                            }
                            break;
                        case 4:
                            isActive = false;
                            break;
                    }
                    await WriteToFile(student);

                    if (choice != 4) General.Sleep();
                }
            }
            Console.Clear();
            Student updatedStudent = ReadFromFile();

            File.WriteAllText(
                "../../../ConsoleLayer/data.json", "");

            return updatedStudent;
        }

        private async ValueTask WriteToFile(Student student)
        {
            File.WriteAllText("../../../ConsoleLayer/data.json",
                JsonConvert.SerializeObject(student, Formatting.Indented));
        }

        private Student ReadFromFile()
        {
            return JsonConvert.DeserializeObject<Student>(
                File.ReadAllText("../../../ConsoleLayer/data.json"));
        }

        private async ValueTask<Student> SelectStudent()
        {
            Console.Write("Enter username: ");
            string user = Console.ReadLine();

            Student maybeStudent =
                await this.studentService
                .RetrieveStudentByUserName(user);

            General.PrintObjectProperties(maybeStudent);

            return maybeStudent;
        }

        private async void AddStudent()
        {
            Console.Write("Nechta Student Objectni Tablega kiritmoqchisiz: ");
            string choice = Console.ReadLine();
            int count;
            int.TryParse(choice, out count);

            if (count is 0) return;

            List<Student> list = new List<Student>();
            Student maybeStudent = new();

            for (int i = 0; i < count; i++)
            {
                maybeStudent = await this.studentService.AddStudentAsync(
                    General.CreateObjectFiller<Student>().Create());
                list.Add(maybeStudent);
            }

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{maybeStudent.Id}\n{maybeStudent.FullName}");
            }
        }

        private async ValueTask<Student> AddStudentMenu()
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

            var student = new Student()
            {
                Id = Guid.NewGuid(),
                FullName = fullName,
                UserName = username.ToLower(),
                BirthDate = birthDate,
                Gender = genderBool
            };

            return student;
        }
    }
}
