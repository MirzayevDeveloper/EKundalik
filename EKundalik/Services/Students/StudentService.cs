// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EKundalik.Brokers.Storages;
using EKundalik.Models.Students;

namespace EKundalik.Services.Students
{
    public partial class StudentService : IStudentService
    {
        private readonly IStorageBroker storageBroker;

        public StudentService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public ValueTask<Student> AddStudentAsync(Student student) =>
        TryCatch(async () =>
        {
            ValidateStudent(student);

            return await this.storageBroker.InsertStudentAsync(student);
        });

        public ValueTask<Student> RetrieveStudentByIdAsync(Guid id) =>
        TryCatch(async () =>
        {
            ValidateStudentId(id);

            Student maybeStudent = await this.storageBroker.SelectStudentByIdAsync(id);

            ValidateStorageStudent(maybeStudent, id);

            return maybeStudent;
        });

        public ValueTask<Student> RetrieveStudentByUserName(string userName) =>
        TryCatch(async () =>
        {
            ValidateStudentUserName(userName);

            Student maybeStudent = 
                await this.storageBroker.SelectStudentByUserNameAsync(userName);

            ValidateStorageStudent(maybeStudent, userName);

            return maybeStudent;
        });

        public IQueryable<Student> RetrieveAllStudents() =>
            this.storageBroker.SelectAllStudents();

        public ValueTask<Student> ModifyStudentAsync(Student student) =>
        TryCatch(async () =>
        {
            ValidateStudent(student);

            Student maybeStudent =
                await this.storageBroker.SelectStudentByIdAsync(student.Id);

            return await this.storageBroker.UpdateStudentAsync(student);
        });
    }
}
