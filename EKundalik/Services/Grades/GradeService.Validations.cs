// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using EKundalik.Models.Grades;
using EKundalik.Models.Grades.Exceptions;
using EKundalik.Models.StudentTeachers;
using EKundalik.Models.StudentTeachers.Exceptions;

namespace EKundalik.Services.Grades
{
    public partial class GradeService
    {
        private static void ValidateGrade(Grade grade)
        {
            ValidateGradeIsNotNull(grade);

            Validate(
                (Rule: IsInvalid(grade.Id), Parameter: nameof(Grade.Id)),
                (Rule: IsInvalid(grade.GradeRate), Parameter: nameof(Grade.GradeRate)),
                (Rule: IsInvalid(grade.Date), Parameter: nameof(Grade.Date)),
                (Rule: IsInvalid(grade.StudentTeacherId), Parameter: nameof(Grade.StudentTeacherId))
                );
        }

        private static void ValidateGradeOnModify(Grade grade)
        {
            ValidateGradeIsNotNull(grade);

            Validate(
               (Rule: IsInvalid(grade.GradeRate), Parameter: nameof(Grade.GradeRate)),
               (Rule: IsInvalid(grade.StudentTeacherId), Parameter: nameof(Grade.StudentTeacherId)));
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidGradeException = new InvalidGradeException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidGradeException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidGradeException.ThrowIfContainsErrors();
        }

        private static void ValidateStorageGrade(Grade maybeGrade, Guid userId)
        {
            if (maybeGrade is null)
            {
                throw new NotFoundGradeException(userId);
            }
        }

        private static void ValidateGradeIsNotNull(Grade Grade)
        {
            if (Grade is null)
            {
                throw new NullGradeException();
            }
        }

        private static void ValidateStorageStudentTeacherIsExists(StudentTeacher maybeStudentTeacher, Guid id)
        {
            if(maybeStudentTeacher is null)
            {
                throw new NotFoundStudentTeacher(id);
            }
        }

        private static void ValidateGradeId(Guid id)
        {
            Validate((Rule: IsInvalid(id), Parameter: nameof(Grade.Id)));
        }

        private static bool IsEnumInvalid<T>(T value)
        {
            bool isDefined = Enum.IsDefined(typeof(T), value);

            return isDefined is false;
        }

        private static dynamic IsInvalid<T>(T value) => new
        {
            Condition = IsEnumInvalid(value),
            Message = "Value is not recognized"
        };

        private static dynamic IsInvalid(DateTime dates) => new
        {
            Condition = dates == default,
            Message = "Date is required"
        };

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == default,
            Message = "Id is required"
        };
    }
}
