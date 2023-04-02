// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EKundalik.Brokers.Storages;
using EKundalik.Models.Subjects;
using EKundalik.Services.Subjects;
using Newtonsoft.Json;

namespace EKundalik.ConsoleLayer
{
    public class SubjectLayer
    {
        private readonly ISubjectService subjectService;

        public SubjectLayer(IStorageBroker storageBroker) =>
            this.subjectService = new SubjectService(storageBroker);

        public async Task SubjectCase()
        {
            bool isActive = true;

            while (isActive)
            {
                Console.Clear();
                int choice = General.PrintCrudOptions(nameof(Subject));
                Console.Beep();

                switch (choice)
                {
                    case 1:
                        {
                            Subject maybeSubject =
                                await AddSubjectMenu();

                            await this.subjectService
                                .AddSubjectAsync(maybeSubject);
                        }
                        break;
                    case 2:
                        {
                            await SelectSubject();
                        }
                        break;
                    case 3:
                        {
                            Subject maybeSubject =
                                await UpdateSubject();

                            Subject storageSubject = await this.subjectService
                                .ModifySubjectAsync(maybeSubject);

                            General.PrintObjectProperties(storageSubject);
                        }
                        break;
                    case 4:
                        {
                            Subject maybeSubject = DeleteSubject();

                            await this.subjectService
                                .RemoveSubjectByIdAsync(maybeSubject.Id);
                        }
                        break;
                    case 5:
                        {
                            IQueryable<Subject> Subjects =
                                this.subjectService.RetrieveAllSubjects();

                            General.SelectAll(Subjects);
                        }
                        break;
                    case 6:
                        AddSubject();
                        break;
                    case 7:
                        isActive = false;
                        break;
                }
                General.Pause();
            }
        }

        private Subject DeleteSubject()
        {
            Subject Subject = SelectSubject().Result ?? new();

            return Subject;
        }

        private async ValueTask<Subject> UpdateSubject()
        {
            Subject subject = SelectSubject().Result;

            if (subject != null)
            {
                Console.Write("Enter new Subject name: ");
                string user = Console.ReadLine();

                subject.SubjectName = user;
            }

            Console.Clear();

            return subject;
        }

        private async ValueTask WriteToFile(Subject Subject)
        {
            File.WriteAllText("../../../ConsoleLayer/data.json",
                JsonConvert.SerializeObject(Subject, Formatting.Indented));
        }

        private Subject ReadFromFile()
        {
            return JsonConvert.DeserializeObject<Subject>(
                File.ReadAllText("../../../ConsoleLayer/data.json"));
        }

        private async ValueTask<Subject> SelectSubject()
        {
            Console.Write("Enter subject name: ");
            string subject = Console.ReadLine();

            Subject maybeSubject =
                await this.subjectService
                .RetrieveSubjectBySubjectName(subject);

            General.PrintObjectProperties(maybeSubject);

            return maybeSubject;
        }

        private async void AddSubject()
        {
            Console.Write("Nechta Subject Objectni Tablega kiritmoqchisiz: ");
            string choice = Console.ReadLine();
            int count;
            int.TryParse(choice, out count);

            if (count is 0) return;

            List<Subject> list = new List<Subject>();
            Subject maybeSubject = new();

            for (int i = 0; i < count; i++)
            {
                maybeSubject = await this.subjectService.AddSubjectAsync(
                    General.CreateObjectFiller<Subject>().Create());
                list.Add(maybeSubject);
            }
        }

        private async ValueTask<Subject> AddSubjectMenu()
        {
            Console.Write("enter subject name: ");
            string subject = Console.ReadLine();

            var Subject = new Subject()
            {
                Id = Guid.NewGuid(),
                SubjectName = subject.ToLower()
            };

            return Subject;
        }
    }
}
