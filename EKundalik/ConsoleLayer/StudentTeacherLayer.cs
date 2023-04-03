// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EKundalik.Brokers.Storages;
using EKundalik.Models.StudentTeachers;
using EKundalik.Services.StudentTeachers;
using Newtonsoft.Json;

namespace EKundalik.ConsoleLayer
{
    public class StudentTeacherLayer
    {
        private readonly IStudentTeacherService studentTeacherService;

        public StudentTeacherLayer(IStorageBroker storageBroker) =>
            this.studentTeacherService = new StudentTeacherService(storageBroker);

        public async Task StudentTeacherCase()
        {
            bool isActive = true;

            while (isActive)
            {
                Console.Clear();
                int choice = General.PrintCrudOptions(nameof(StudentTeacher));
                Console.Beep();

                switch (choice)
                {
                    case 1:
                        {
                            StudentTeacher maybeStudentTeacher =
                                await AddStudentTeacherMenu();

                            await this.studentTeacherService
                                .AddStudentTeacherAsync(maybeStudentTeacher);
                        }
                        break;
                    case 2:
                        {
                            await SelectStudentTeacher();
                        }
                        break;
                    case 3:
                        {
                            StudentTeacher maybeStudentTeacher =
                                await UpdateStudentTeacher();

                            StudentTeacher storageStudentTeacher = await this.studentTeacherService
                                .ModifyStudentTeacherAsync(maybeStudentTeacher);

                            General.PrintObjectProperties(storageStudentTeacher);
                        }
                        break;
                    case 4:
                        {
                            StudentTeacher maybeStudentTeacher = DeleteStudentTeacher();

                            await this.studentTeacherService
                                .RemoveStudentTeacherByIdAsync(maybeStudentTeacher.Id);
                        }
                        break;
                    case 5:
                        {
                            IQueryable<StudentTeacher> StudentTeachers =
                                this.studentTeacherService.RetrieveAllStudentTeachers();

                            General.SelectAll(StudentTeachers);
                        }
                        break;
                    case 6:
                        AddStudentTeacher();
                        break;
                    case 7:
                        isActive = false;
                        break;
                }
                General.Pause();
            }
        }

        private StudentTeacher DeleteStudentTeacher()
        {
            StudentTeacher StudentTeacher = SelectStudentTeacher().Result ?? new();

            return StudentTeacher;
        }

        private async ValueTask<StudentTeacher> UpdateStudentTeacher()
        {
            bool isActive = true;
            StudentTeacher studentTeacher = SelectStudentTeacher().Result;

            if (studentTeacher != null)
            {
                await WriteToFile(studentTeacher);
            }

            while (isActive && studentTeacher != null)
            {
                if (ReadFromFile().Id != default)
                { 
                    Console.Clear();
                    General.PrintObjectProperties(studentTeacher);

                    Console.Write("1.StudentId\n" +
                                    "2.TeacherId\n" +
                                    "3.SubjectId\n" +
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
                                Console.Write("enter StudentId: ");
                                string studentId = Console.ReadLine();
                                Guid id1;
                                Guid.TryParse(studentId, out id1);
                            }
                            break;
                        case 2:
                            {
                                Console.Write("enter TeacherId: ");
                                string teacherId = Console.ReadLine();

                                Guid id2;
                                Guid.TryParse(teacherId, out id2);
                            }
                            break;
                        case 3:
                            {
                                Console.Write("enter SubjectId: ");
                                string subjectId = Console.ReadLine();
                                Guid id3;
                                Guid.TryParse(subjectId, out id3);
                            }
                            break;
                        case 4:
                            isActive = false;
                            break;
                    }
                    await WriteToFile(studentTeacher);

                    if (choice != 4 && choice != 0) General.Sleep();
                }
            }
            Console.Clear();
            StudentTeacher updatedStudentTeacher = ReadFromFile();

            File.WriteAllText(
                "../../../ConsoleLayer/data.json", "");

            return updatedStudentTeacher;
        }

        private async ValueTask WriteToFile(StudentTeacher StudentTeacher)
        {
            File.WriteAllText("../../../ConsoleLayer/data.json",
                JsonConvert.SerializeObject(StudentTeacher, Formatting.Indented));
        }

        private StudentTeacher ReadFromFile()
        {
            return JsonConvert.DeserializeObject<StudentTeacher>(
                File.ReadAllText("../../../ConsoleLayer/data.json"));
        }

        private async ValueTask<StudentTeacher> SelectStudentTeacher()
        {
            Console.Write("Enter student teacher id: ");
            string id = Console.ReadLine();
            Guid studentTeacherId;
            Guid.TryParse(id, out studentTeacherId);

            StudentTeacher maybeStudentTeacher =
                await this.studentTeacherService
                    .RetrieveStudentTeacheryIdAsync(studentTeacherId);

            General.PrintObjectProperties(maybeStudentTeacher);

            return maybeStudentTeacher;
        }

        private async void AddStudentTeacher()
        {
            Console.Write("Nechta StudentTeacher Objectni Tablega kiritmoqchisiz: ");
            string choice = Console.ReadLine();
            int count;
            int.TryParse(choice, out count);

            if (count is 0) return;

            List<StudentTeacher> list = new List<StudentTeacher>();
            StudentTeacher maybeStudentTeacher = new();

            for (int i = 0; i < count; i++)
            {
                maybeStudentTeacher = await this.studentTeacherService.AddStudentTeacherAsync(
                    General.CreateObjectFiller<StudentTeacher>().Create());
                list.Add(maybeStudentTeacher);
            }

            Console.WriteLine($"Successfully added {count} objects...");
            Console.ReadKey();
        }

        private async ValueTask<StudentTeacher> AddStudentTeacherMenu()
        {
            Console.Write("enter StudentId: ");
            string studentId = Console.ReadLine();
            Guid id1;
            Guid.TryParse(studentId, out id1);

            Console.Write("enter TeacherId: ");
            string teacherId = Console.ReadLine();
            Guid id2;
            Guid.TryParse(studentId, out id2);

            Console.Write("enter SubjectId: ");
            string subjectId = Console.ReadLine();
            Guid id3;
            Guid.TryParse(studentId, out id3);

            var StudentTeacher = new StudentTeacher()
            {
                Id = Guid.NewGuid(),
                StudentId = id1,
                TeacherId = id2,
                SubjectId = id3
            };

            return StudentTeacher;
        }


    }
}
