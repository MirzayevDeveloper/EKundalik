// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Collections.Generic;
using EKundalik.Brokers.Storages;
using EKundalik.Models.Students;
using EKundalik.Services.Students;
using Tynamix.ObjectFiller;

namespace EKundalik
{
    public partial class ProgramHelper
    {
        private readonly IStorageBroker storageBroker;
        private readonly IStudentService studentService;

        public ProgramHelper()
        {
            this.storageBroker = new StorageBroker();

            this.studentService =
                new StudentService(this.storageBroker);
        }

        public void FirstMenuAsync()
        {
            bool isActive = true;

            while (isActive)
            {
                Console.Clear();

                Console.Write("1.Student\n2.Teacher\n3.Student Teacher\n" +
                    "4.Subject\n5.Grade\nchoose option: ");

                string choose = Console.ReadLine();
                int choice;
                int.TryParse(choose, out choice);

                switch (choice)
                {
                    case 1:
                        CrudMenuAsync("Student");
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                }
            }
        }

        private void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        private async void AddStudent(int count)
        {
            List<Student> list = new List<Student>();
            Student maybeStudent = new();
            for (int i = 0; i < count; i++)
            {
                maybeStudent = await this.studentService.AddStudentAsync(
                    CreateObjectFiller<Student>().Create());
                list.Add(maybeStudent);
            }

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{maybeStudent.Id}\n{maybeStudent.FullName}");
            }
        }
        private Filler<T> CreateObjectFiller<T>() where T : class
        {
            var filler = new Filler<T>();

            filler.Setup().OnType<DateTime>()
                .Use(new DateTimeRange(DateTime.UnixEpoch).GetValue);

            return filler;
        }
    }
}
