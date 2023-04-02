// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using EKundalik.Models.Grades;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace EKundalik.Brokers.Storages
{
    public partial class StorageBroker
    {
        private const string gradeTable = "Grades";
        public async ValueTask<Grade> InsertGradeAsync(Grade Grade)
        {
            string columns = "id, graderate, date, studentsteachersid";
            string values = "@Id, @GradeRate, @Date, @StudentTeacherId";

            return await this.InsertAsync(Grade, gradeTable, (columns, values));
        }

        public async ValueTask<Grade> SelectGradeByIdAsync(Guid id) =>
            await this.SelectByIdAsync<Grade>(id, gradeTable);

        public IQueryable<Grade> SelectAllGrades() =>
            SelectAll<Grade>(gradeTable);

        public async ValueTask<Grade> UpdateGradeAsync(Grade Grade) =>
            await UpdateAsync(Grade, gradeTable);

        public async ValueTask<Grade> DeleteGradeAsync(Grade Grade)
        {
            await DeleteAsync<Grade>(Grade.Id);

            return Grade;
        }
    }
}
