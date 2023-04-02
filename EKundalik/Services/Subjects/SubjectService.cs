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

        public ValueTask<Subject> AddSubjectAsync(Subject Subject) =>
        TryCatch(async () =>
        {
            ValidateSubject(Subject);

            Subject maybeSubject =
                await this.storageBroker.SelectSubjectBySubjectNameAsync(Subject.SubjectName);

            ValidateSubjectIsNotExists(Subject, maybeSubject);

            return await this.storageBroker.InsertSubjectAsync(Subject);
        });

        public ValueTask<Subject> RetrieveSubjectByIdAsync(Guid id) =>
        TryCatch(async () =>
        {
            ValidateSubjectId(id);

            Subject maybeSubject = await this.storageBroker.SelectSubjectByIdAsync(id);

            ValidateStorageSubject(maybeSubject, id);

            return maybeSubject;
        });

        public ValueTask<Subject> RetrieveSubjectBySubjectName(string SubjectName) =>
        TryCatch(async () =>
        {
            ValidateSubjectName(SubjectName);

            Subject maybeSubject =
                await this.storageBroker.SelectSubjectBySubjectNameAsync(SubjectName);

            ValidateStorageSubject(maybeSubject, SubjectName);

            return maybeSubject;
        });

        public IQueryable<Subject> RetrieveAllSubjects() =>
            this.storageBroker.SelectAllSubjects();

        public ValueTask<Subject> ModifySubjectAsync(Subject Subject) =>
        TryCatch(async () =>
        {
            ValidateSubject(Subject);

            Subject maybeSubject =
                await this.storageBroker.SelectSubjectByIdAsync(Subject.Id);

            return await this.storageBroker.UpdateSubjectAsync(Subject);
        });

        public ValueTask<Subject> RemoveSubjectByIdAsync(Guid SubjectId) =>
        TryCatch(async () =>
        {
            ValidateSubjectId(SubjectId);

            Subject maybeSubject =
                await this.storageBroker
                    .SelectSubjectByIdAsync(SubjectId);

            ValidateStorageSubject(maybeSubject, SubjectId);

            return await this.storageBroker
                .DeleteSubjectAsync(maybeSubject);
        });
    }
}
