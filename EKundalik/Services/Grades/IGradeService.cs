// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using EKundalik.Models.Grades;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace EKundalik.Services.Grades
{
    public interface IGradeService
    {
        ValueTask<Grade> AddGradeAsync(Grade grade);
        ValueTask<Grade> RetrieveGradeByIdAsync(Guid gradeId);
        IQueryable<Grade> RetrieveAllGrades();
        ValueTask<Grade> ModifyGradeAsync(Grade grade);
        ValueTask<Grade> RemoveGradeByIdAsync(Guid gradeId);
    }
}
