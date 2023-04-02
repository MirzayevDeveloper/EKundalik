// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EKundalik.Brokers.Storages;
using EKundalik.Models.Teachers;

namespace EKundalik.Services.Teachers
{
    public partial class TeacherService : ITeacherService
    {
        private readonly IStorageBroker storageBroker;

        public TeacherService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public ValueTask<Teacher> AddTeacherAsync(Teacher Teacher) =>
        TryCatch(async () =>
        {
            ValidateTeacher(Teacher);

            Teacher maybeTeacher =
                await this.storageBroker.SelectTeacherByUserNameAsync(Teacher.UserName);

            ValidateUserNameIsNotExists(Teacher, maybeTeacher);

            return await this.storageBroker.InsertTeacherAsync(Teacher);
        });

        public ValueTask<Teacher> RetrieveTeacherByIdAsync(Guid id) =>
        TryCatch(async () =>
        {
            ValidateTeacherId(id);

            Teacher maybeTeacher = await this.storageBroker.SelectTeacherByIdAsync(id);

            ValidateStorageTeacher(maybeTeacher, id);

            return maybeTeacher;
        });

        public ValueTask<Teacher> RetrieveTeacherByUserName(string userName) =>
        TryCatch(async () =>
        {
            ValidateTeacherUserName(userName);

            Teacher maybeTeacher =
                await this.storageBroker.SelectTeacherByUserNameAsync(userName);

            ValidateStorageTeacher(maybeTeacher, userName);

            return maybeTeacher;
        });

        public IQueryable<Teacher> RetrieveAllTeachers() =>
            this.storageBroker.SelectAllTeachers();

        public ValueTask<Teacher> ModifyTeacherAsync(Teacher Teacher) =>
        TryCatch(async () =>
        {
            ValidateTeacherOnModify(Teacher);

            Teacher maybeTeacher =
                await this.storageBroker.SelectTeacherByIdAsync(Teacher.Id);

            return await this.storageBroker.UpdateTeacherAsync(Teacher);
        });

        public ValueTask<Teacher> RemoveTeacherByIdAsync(Guid TeacherId) =>
        TryCatch(async () =>
        {
            ValidateTeacherId(TeacherId);

            Teacher maybeTeacher =
                await this.storageBroker
                    .SelectTeacherByIdAsync(TeacherId);

            ValidateStorageTeacher(maybeTeacher, TeacherId);

            return await this.storageBroker
                .DeleteTeacherAsync(maybeTeacher);
        });
    }
}
