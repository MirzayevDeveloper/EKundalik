// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System.Threading.Tasks;
using EKundalik.Models.Students;

namespace EKundalik.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<Student> InsertStudentAsync(Student student)
        {
            string columns = "id, full_name, birth_date, gender";
            string values = "@Id, @FullName, @BirthDate, @Gender";

            return await this.InsertAsync(student, "student", (columns, values));
        }
    }
}
