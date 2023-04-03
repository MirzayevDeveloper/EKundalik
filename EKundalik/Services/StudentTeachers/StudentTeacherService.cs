// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using EKundalik.Brokers.Storages;
using EKundalik.Models.StudentTeachers;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace EKundalik.Services.StudentTeachers
{
    public partial class StudentTeacherService : IStudentTeacherService
    {
        private readonly IStorageBroker storageBroker;

        public StudentTeacherService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public ValueTask<StudentTeacher> AddStudentTeacherAsync(StudentTeacher studentTeacher) =>
        TryCatch(async () =>
        {
            ValidateStudentTeacher(studentTeacher);

            return await this.storageBroker.InsertStudentTeacherAsync(studentTeacher);
        });

        public ValueTask<StudentTeacher> RetrieveStudentTeacherIdAsync(Guid studentTeacherId) =>
        TryCatch(async () =>
        {
            ValidateStudentTeacherId(studentTeacherId);

            StudentTeacher maybeStudentTeacher = 
                await this.storageBroker.SelectStudentTeacherByIdAsync(studentTeacherId);

            ValidateStorageStudentTeacher(maybeStudentTeacher, studentTeacherId);

            return maybeStudentTeacher;
        });

        public IQueryable<StudentTeacher> RetrieveAllStudentTeachers() =>
            this.storageBroker.SelectAllStudentTeachers();

        public ValueTask<StudentTeacher> ModifyStudentTeacherAsync(StudentTeacher studentTeacher) =>
        TryCatch(async () =>
        {
            ValidateStudentTeacherOnModify(studentTeacher);

            StudentTeacher maybeStudentTeacher =
                await this.storageBroker.SelectStudentTeacherByIdAsync(studentTeacher.Id);

            ValidateStorageStudentTeacher(maybeStudentTeacher, studentTeacher.Id);

            return await this.storageBroker.UpdateStudentTeacherAsync(studentTeacher);
        });

        public ValueTask<StudentTeacher> RemoveStudentTeacherByIdAsync(Guid studentTeacherId) =>
        TryCatch(async () =>
        {
            ValidateStudentTeacherId(studentTeacherId);

            StudentTeacher maybeStudentTeacher =
                await this.storageBroker
                    .SelectStudentTeacherByIdAsync(studentTeacherId);

            ValidateStorageStudentTeacher(maybeStudentTeacher, studentTeacherId);

            return await this.storageBroker
                .DeleteStudentTeacherAsync(maybeStudentTeacher);
        });
    }
}
