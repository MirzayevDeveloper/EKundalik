// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Threading.Tasks;
using EKundalik.Models.Students;

namespace EKundalik.Brokers.Storages
{
    public partial class StorageBroker
    {
        private const string tableName = "student";
        public async ValueTask<Student> InsertStudentAsync(Student student)
        {
            string columns = "id, full_name, user_name, birth_date, gender";
            string values = "@Id, @FullName, @UserName, @BirthDate, @Gender";
            student.UserName = student.UserName.ToLower();

            return await this.InsertAsync(student, tableName, (columns, values));
        }

        public async ValueTask<Student> SelectStudentByIdAsync(Guid id) =>
            await this.SelectByIdAsync<Student>(id, tableName);

        
    }
}
