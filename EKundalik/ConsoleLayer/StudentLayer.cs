// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EKundalik.Brokers.Storages;
using EKundalik.Models.Students;
using EKundalik.Services.Students;
using Tynamix.ObjectFiller;

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
                            AddStudentMenu();
                        }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                }
                General.Pause();
            }
        }

        private async void AddStudent(int count)
        {
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

        private async void AddStudentMenu()
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


            Student maybeStudent = 
                await this.studentService.AddStudentAsync(student);

            General.PrintObjectProperties(maybeStudent);
        }
    }
}
