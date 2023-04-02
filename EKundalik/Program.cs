// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using EKundalik.Brokers.Storages;
using EKundalik.ConsoleLayer;

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

                Console.Write("1.Student\n" +
                              "2.Teacher\n" +
                              "3.Student Teacher\n" +
                              "4.Subject\n" +
                              "5.Grade\n" +
                              "6.Exit\n" +
                              "choose option: ");

                string choose = Console.ReadLine();
                int choice;
                int.TryParse(choose, out choice);
                Console.Beep();

                switch (choice)
                {
                    case 1:
                        {
                            var studentLayer =
                                new StudentLayer(storageBroker);

                            studentLayer.StudentCase().Wait();
                        }
                        break;
                    case 2:
                        {
                            var teacherLayer =
                                new TeacherLayer(storageBroker);

                            teacherLayer.TeacherCase().Wait();
                        }
                        break;
                    case 3:
                        {
                            var studentTeacherLayer =
                                new StudentTeacherLayer(storageBroker);
                        }
                        break;
                    case 4:
                        {
                            var subjectLayer =
                                new SubjectLayer(storageBroker);

                            subjectLayer.SubjectCase().Wait();
                        }
                        break;
                    case 5:
                        {
                            var gradeLayer =
                                new GradeLayer(storageBroker);
                        }
                        break;
                    case 6:
                        isActive = false;
                        break;
                }
            }
        }
    }
}