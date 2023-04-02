// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EKundalik.Models.Students;

namespace EKundalik.Brokers.Storages
{
    public partial class StorageBroker
    {
        private const string studentTable = "student";
        public async ValueTask<Student> InsertStudentAsync(Student student)
        {
            string columns = "id, fullname, username, birthdate, gender";
            string values = "@Id, @FullName, @UserName, @BirthDate, @Gender";
            student.UserName = student.UserName.ToLower();

            return await this.InsertAsync(student, studentTable, (columns, values));
        }

        public async ValueTask<Student> SelectStudentByIdAsync(Guid id) =>
            await this.SelectByIdAsync<Student>(id, studentTable);

        public async ValueTask<Student> SelectStudentByUserNameAsync(string userName) =>
            await SelectObjectByUserName<Student>(userName, studentTable);

        public IQueryable<Student> SelectAllStudents() =>
            SelectAll<Student>(studentTable);

        public async ValueTask<Student> UpdateStudentAsync(Student student) =>
            await UpdateAsync<Student>(student, studentTable);
    }
}
