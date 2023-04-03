// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EKundalik.Brokers.Storages;
using EKundalik.Models.Grades;
using EKundalik.Models.StudentTeachers;

namespace EKundalik.Services.Grades
{
    public partial class GradeService : IGradeService
    {
        private readonly IStorageBroker storageBroker;

        public GradeService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public ValueTask<Grade> AddGradeAsync(Grade grade) =>
        TryCatch(async () =>
        {
            ValidateGrade(grade);

            Grade maybeGrade =
                await this.storageBroker
                    .SelectGradeByIdAsync(grade.Id);

            StudentTeacher maybeStudentTeacher = 
                await this.storageBroker
                    .SelectStudentTeacherByIdAsync(
                        grade.StudentTeacherId);

            ValidateStudentTeacherExistsOrNot(
                maybeStudentTeacher, grade.StudentTeacherId);

            return await this.storageBroker
                .InsertGradeAsync(grade);
        });

        public ValueTask<Grade> RetrieveGradeByIdAsync(Guid gradeId) =>
        TryCatch(async () =>
        {
            ValidateGradeId(gradeId);

            Grade maybeGrade =
                await this.storageBroker
                    .SelectGradeByIdAsync(gradeId);

            ValidateStorageGrade(maybeGrade, gradeId);

            return maybeGrade;
        });

        public IQueryable<Grade> RetrieveAllGrades() =>
            this.storageBroker.SelectAllGrades();

        public ValueTask<Grade> ModifyGradeAsync(Grade grade) =>
        TryCatch(async () =>
        {
            ValidateGradeOnModify(grade);

            Grade maybeGrade =
                await this.storageBroker
                    .SelectGradeByIdAsync(grade.Id);

            ValidateStorageGrade(maybeGrade, grade.Id);

            StudentTeacher maybeStudentTeacher =
                await this.storageBroker
                    .SelectStudentTeacherByIdAsync(
                        grade.StudentTeacherId);

            ValidateStudentTeacherExistsOrNot(
                maybeStudentTeacher, grade.StudentTeacherId);

            return await this.storageBroker
                .UpdateGradeAsync(grade);
        });

        public ValueTask<Grade> RemoveGradeByIdAsync(Guid gradeId) =>
        TryCatch(async () =>
        {
            ValidateGradeId(gradeId);

            Grade maybeGrade =
                await this.storageBroker
                    .SelectGradeByIdAsync(gradeId);

            ValidateStorageGrade(maybeGrade, gradeId);

            return await this.storageBroker
                .DeleteGradeAsync(maybeGrade);
        });
    }
}
