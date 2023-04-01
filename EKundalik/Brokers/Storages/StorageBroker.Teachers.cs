// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System.Threading.Tasks;
using EKundalik.Models.Teachers;

namespace EKundalik.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<Teacher> InsertTeacherAsync(Teacher teacher)
        {
            string columns = "id, full_name, user_name ,birth_date, gender";
            string values = "@Id, @FullName, @UserName ,@BirthDate, @Gender";

            return await this.InsertAsync(teacher, "teacher", (columns, values));
        }
    }
}
