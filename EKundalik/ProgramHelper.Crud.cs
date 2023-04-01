// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using EKundalik.Models.Students;

namespace EKundalik
{
    public partial class ProgramHelper
    {
        public void CrudMenuAsync(string name)
        {
            bool isActive = true;

            while (isActive)
            {
                Console.Clear();
                Console.Write($"1.Create {name}\n2.Select {name}\n3.Update {name}\n" +
                    $"4.Delete {name}\n5.Select All {name}\n6.Add random {name}\nchoose option: ");

                string choose = Console.ReadLine();
                int choice;
                int.TryParse(choose, out choice);

                switch (choice)
                {
                    case 1:
                        Create(name);
                        Pause();
                        break;
                    case 2:
                        Select(name);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        AddRandomStudent();
                        break;
                }
            }
        }

        public async void Select(string name)
        {
            Console.Write("Enter userName: ");
            string userName = Console.ReadLine();

            var student = new Student() { UserName = userName };

            Student maybe = await this.studentService
                .RetrieveStudentByIdAsync(student);

            if (maybe is not null)
            {

                await Console.Out.WriteLineAsync($"Id: {maybe.Id}\n" +
                    $"Full name: {maybe.FullName}\nUsername: {maybe.UserName}\n" +
                    $"Birth date: {maybe.BirthDate}\nGender: {maybe.Gender}");
            }
            Pause();
        }

        public async void Create(string name)
        {
            switch (name.ToLower())
            {
                case "student":
                case "teacher":
                    {
                        Console.Write("enter Full name: ");
                        string fullName = Console.ReadLine();

                        Console.Write("create username: ");
                        string username = Console.ReadLine();

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
                            UserName = username,
                            BirthDate = birthDate,
                            Gender = genderBool
                        };

                        Student maybeStudent =
                            await this.studentService.AddStudentAsync(student);

                        if (maybeStudent is not null)
                        {
                            Console.WriteLine($"Successfully created {name}");
                        }
                    }
                    break;
                case "student_teacher":

                    break;
                case "subject":
                    break;
                case "grade":
                    break;
            }
        }

        public void AddRandomStudent()
        {
            Console.Write("nechta student qushmoqchisiz: ");
            string count = Console.ReadLine();

            int choice;
            int.TryParse(count, out choice);

            if (choice is 0) return;

            AddStudent(choice);
        }
    }
}
