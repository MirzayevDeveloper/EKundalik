// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EKundalik.Brokers.Storages;
using EKundalik.Models.Subjects;

namespace EKundalik.Services.Subjects
{
    public partial class SubjectService : ISubjectService
    {
        private readonly IStorageBroker storageBroker;

        public SubjectService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public ValueTask<Subject> AddSubjectAsync(Subject subject) =>
        TryCatch(async () =>
        {
            ValidateSubject(subject);

            Subject maybeSubject =
                await this.storageBroker.SelectSubjectBySubjectNameAsync(subject.SubjectName);

            ValidateSubjectIsNotExists(subject, maybeSubject);

            return await this.storageBroker.InsertSubjectAsync(subject);
        });

        public ValueTask<Subject> RetrieveSubjectByIdAsync(Guid id) =>
        TryCatch(async () =>
        {
            ValidateSubjectId(id);

            Subject maybeSubject = await this.storageBroker.SelectSubjectByIdAsync(id);

            ValidateStorageSubject(maybeSubject, id);

            return maybeSubject;
        });

        public ValueTask<Subject> RetrieveSubjectBySubjectName(string subjectName) =>
        TryCatch(async () =>
        {
            ValidateSubjectName(subjectName);

            Subject maybeSubject =
                await this.storageBroker.SelectSubjectBySubjectNameAsync(subjectName);

            ValidateStorageSubject(maybeSubject, subjectName);

            return maybeSubject;
        });

        public IQueryable<Subject> RetrieveAllSubjects() =>
            this.storageBroker.SelectAllSubjects();

        public ValueTask<Subject> ModifySubjectAsync(Subject subject) =>
        TryCatch(async () =>
        {
            ValidateSubject(subject);

            Subject maybeSubject =
                await this.storageBroker.SelectSubjectByIdAsync(subject.Id);

            return await this.storageBroker.UpdateSubjectAsync(subject);
        });

        public ValueTask<Subject> RemoveSubjectByIdAsync(Guid subjectId) =>
        TryCatch(async () =>
        {
            ValidateSubjectId(subjectId);

            Subject maybeSubject =
                await this.storageBroker
                    .SelectSubjectByIdAsync(subjectId);

            ValidateStorageSubject(maybeSubject, subjectId);

            return await this.storageBroker
                .DeleteSubjectAsync(maybeSubject);
        });
    }
}
