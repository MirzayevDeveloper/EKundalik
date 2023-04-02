// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EKundalik.Models.Teachers;

namespace EKundalik.Brokers.Storages
{
    public partial class StorageBroker
    {
        private const string teacherTable = "teachers";
        public async ValueTask<Teacher> InsertTeacherAsync(Teacher Teacher)
        {
            string columns = "id, fullname, username, birthdate, gender";
            string values = "@Id, @FullName, @UserName, @BirthDate, @Gender";
            Teacher.UserName = Teacher.UserName.ToLower();

            return await this.InsertAsync(Teacher, teacherTable, (columns, values));
        }

        public async ValueTask<Teacher> SelectTeacherByIdAsync(Guid id) =>
            await this.SelectByIdAsync<Teacher>(id, teacherTable);

        public async ValueTask<Teacher> SelectTeacherByUserNameAsync(string userName) =>
            await SelectObjectByUserName<Teacher>(userName, teacherTable);

        public IQueryable<Teacher> SelectAllTeachers() =>
            SelectAll<Teacher>(teacherTable);

        public async ValueTask<Teacher> UpdateTeacherAsync(Teacher Teacher) =>
            await UpdateAsync(Teacher, teacherTable);

        public async ValueTask<Teacher> DeleteTeacherAsync(Teacher Teacher)
        {
            await DeleteAsync<Teacher>(Teacher.Id);

            return Teacher;
        }
    }
}
