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
        public async ValueTask<Student> InsertStudentAsync(Student student)
        {
            string columns = "id, full_name, user_name, birth_date, gender";
            string values = "@Id, @FullName, @UserName, @BirthDate, @Gender";

            return await this.InsertAsync(student, "student", (columns, values));
        }

        public async ValueTask<Student> SelectStudentByIdAsync(Student student, Guid id = default, string userName = "")
        {
            return await this.SelectByIdAsync(id, "student", student, idColumnName: "user_name", secondOption: student.UserName);
        }
    }
}
