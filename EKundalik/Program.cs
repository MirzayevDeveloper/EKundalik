// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using EKundalik.Brokers.Storages;
using EKundalik.ConsoleLayer;
using EKundalik.Models.Grades;
using EKundalik.Models.Students;
using EKundalik.Models.StudentTeachers;
using EKundalik.Models.Subjects;
using EKundalik.Models.Teachers;

namespace EKundalik
{
    public class Program
    {
        private static IStorageBroker storageBroker;
        static void Main(string[] args)
        {
            storageBroker = new StorageBroker();

            bool isActive = true;

            while (isActive)
            {
                Console.Clear();

                Console.Write("1.Student\n2.Teacher\n3.Student Teacher\n" +
                    "4.Subject\n5.Grade\n6.Exit\nchoose option: ");

                string choose = Console.ReadLine();
                int choice;
                int.TryParse(choose, out choice);
                Console.Beep();

                switch (choice)
                {
                    case 1:
                        {
                            SwitchCaseAsync(nameof(Student));
                        }
                        break;
                    case 2:
                        {
                            SwitchCaseAsync(nameof(Teacher));
                        }
                        break;
                    case 3:
                        {
                            SwitchCaseAsync(nameof(StudentTeacher));
                        }
                        break;
                    case 4:
                        {
                            SwitchCaseAsync(nameof(Subject));
                        }
                        break;
                    case 5:
                        {
                            SwitchCaseAsync(nameof(Grade));
                        }
                        break;
                    case 6:
                        isActive = false;
                        break;
                }
            }
        }

        /**************************************************************************************/

        public static async void SwitchCaseAsync(string name)
        {
            switch (name)
            {
                case "Student":
                    {
                        var studentLayer =
                                new StudentLayer(storageBroker);

                        await studentLayer.StudentCase();
                    }
                    break;
                case "Teacher":
                    {
                        var teacherLayer =
                                new TeacherLayer(storageBroker);
                    }
                    break;
                case "StudentTeacher":
                    {
                        var studentTeacherLayer =
                                new StudentTeacherLayer(storageBroker);
                    }
                    break;
                case "Subject":
                    {
                        var subjectLayer =
                                new SubjectLayer(storageBroker);
                    }
                    break;
                case "Grade":
                    {
                        var gradeLayer =
                                new GradeLayer(storageBroker);
                    }
                    break;
            }
        }
    }
}