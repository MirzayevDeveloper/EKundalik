// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using EKundalik.Brokers.Storages;
using EKundalik.Models.Grades;
using Newtonsoft.Json;
using EKundalik.Services.Grades;
using System.IO;
using System.Diagnostics;

namespace EKundalik.ConsoleLayer
{
    public class GradeLayer
    {
        private readonly IGradeService gradeService;
        public GradeLayer(IStorageBroker storageBroker) =>
            this.gradeService = new GradeService(storageBroker);

        public async Task GradeCase()
        {
            bool isActive = true;

            while (isActive)
            {
                Console.Clear();
                int choice = General.PrintCrudOptions(nameof(Grade));
                Console.Beep();

                switch (choice)
                {
                    case 1:
                        {
                            Grade maybeGrade =
                                await AddGradeMenu();

                            await this.gradeService
                                .AddGradeAsync(maybeGrade);
                        }
                        break;
                    case 2:
                        {
                            await SelectGrade();
                        }
                        break;
                    case 3:
                        {
                            Grade maybeGrade =
                                await UpdateGrade();

                            Grade storageGrade = await this.gradeService
                                .ModifyGradeAsync(maybeGrade);

                            General.PrintObjectProperties(storageGrade);
                        }
                        break;
                    case 4:
                        {
                            Grade maybeGrade = DeleteGrade();

                            await this.gradeService
                                .RemoveGradeByIdAsync(maybeGrade.Id);
                        }
                        break;
                    case 5:
                        {
                            IQueryable<Grade> Grades =
                                this.gradeService.RetrieveAllGrades();

                            General.SelectAll(Grades);
                        }
                        break;
                    case 6:
                        AddGrade();
                        break;
                    case 7:
                        isActive = false;
                        break;
                }
                General.Pause();
            }
        }

        private Grade DeleteGrade()
        {
            Grade Grade = SelectGrade().Result ?? new();

            return Grade;
        }

        private async ValueTask<Grade> UpdateGrade()
        {
            bool isActive = true;
            Grade grade = SelectGrade().Result;

            if (grade != null)
            {
                await WriteToFile(grade);
            }

            while (isActive && grade != null)
            {
                if (ReadFromFile().Id != default)
                {
                    Console.Clear();
                    General.PrintObjectProperties(grade);

                    Console.Write("1.Grade Rate\n" +
                                  "2.Student Teacher Id\n" +
                                  "3.Back\n" +
                                    "Which property do you want to change: ");

                    string choose = Console.ReadLine();
                    int choice;
                    int.TryParse(choose, out choice);

                    Console.Beep();

                    switch (choice)
                    {
                        case 1:
                            {
                                
                                Console.Write("0, 1 2, 3, 4, 5, 6\nchoose one: ");
                                string rate = Console.ReadLine();
                                int option;
                                int .TryParse(rate, out option);
                                grade.GradeRate = (GradeEnum)option;

                            }
                            break;
                        case 2:
                            {
                                Console.Write("Enter new Student Teacher id: ");
                                string name = Console.ReadLine();
                                Guid maybeGuid;
                                Guid.TryParse(name, out maybeGuid);

                                grade.StudentTeacherId = maybeGuid;
                            }
                            break;
                        case 3:
                            isActive = false;
                            break;
                    }
                    await WriteToFile(grade);

                    if (choice != 3 && choice != 0) General.Sleep();
                }
            }
            Console.Clear();
            Grade updatedGrade = ReadFromFile();

            File.WriteAllText(
                "../../../ConsoleLayer/data.json", "");

            return updatedGrade;
        }

        private async ValueTask WriteToFile(Grade Grade)
        {
            File.WriteAllText("../../../ConsoleLayer/data.json",
                JsonConvert.SerializeObject(Grade, Formatting.Indented));
        }

        private Grade ReadFromFile()
        {
            return JsonConvert.DeserializeObject<Grade>(
                File.ReadAllText("../../../ConsoleLayer/data.json"));
        }

        private async ValueTask<Grade> SelectGrade()
        {
            Console.Write("Enter grade id: ");
            string id = Console.ReadLine();

            Guid guid;
            Guid.TryParse(id, out guid);

            Grade maybeGrade =
                await this.gradeService
                .RetrieveGradeByIdAsync(guid);

            General.PrintObjectProperties(maybeGrade);

            return maybeGrade;
        }

        private async void AddGrade()
        {
            Console.Write("Nechta Grade Objectni Tablega kiritmoqchisiz: ");
            string choice = Console.ReadLine();
            int count;
            int.TryParse(choice, out count);

            if (count is 0) return;

            List<Grade> list = new List<Grade>();
            Grade maybeGrade = new();

            for (int i = 0; i < count; i++)
            {
                maybeGrade = await this.gradeService.AddGradeAsync(
                    General.CreateObjectFiller<Grade>().Create());
                list.Add(maybeGrade);
            }

            Console.WriteLine($"Successfully added {count} objects...");
            Console.ReadKey();
        }

        private async ValueTask<Grade> AddGradeMenu()
        {
            Console.Write("enter StudentTeacherId: ");
            string id = Console.ReadLine();
            Guid guid;
            Guid.TryParse(id, out guid);

            Console.Write("0, 1 2, 3, 4, 5, 6\nchoose one: ");
            string rate = Console.ReadLine();
            int option;
            int.TryParse(rate, out option);
            GradeEnum gradeEnum = (GradeEnum)option;

            var Grade = new Grade()
            {
                Id = Guid.NewGuid(),
                GradeRate = gradeEnum,
                Date = DateTime.Now,
                StudentTeacherId = guid
            };

            return Grade;
        }
    }
}
