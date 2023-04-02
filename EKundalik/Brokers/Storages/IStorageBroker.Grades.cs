// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using EKundalik.Models.Grades;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace EKundalik.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Grade> InsertGradeAsync(Grade Grade);
        ValueTask<Grade> SelectGradeByIdAsync(Guid id);
        IQueryable<Grade> SelectAllGrades();
        ValueTask<Grade> UpdateGradeAsync(Grade Grade);
        ValueTask<Grade> DeleteGradeAsync(Grade Grade);
    }
}
