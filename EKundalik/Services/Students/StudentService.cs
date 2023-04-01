// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
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
            ValidateStudentOnAdd(student);

            return await this.storageBroker.InsertStudentAsync(student);
        });

        public ValueTask<Student> RetrieveStudentByIdAsync(Student student, Guid studentId = default) =>
        TryCatch(async () =>
        {
            ValidateStudentUserName(student);

            Student maybeStudent = await this.storageBroker.SelectStudentByIdAsync(student);

            ValidateStorageStudent(maybeStudent, student.UserName);

            return maybeStudent;
        });
    }
}
