// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using EKundalik.Models.StudentTeachers;
using EKundalik.Models.StudentTeachers.Exceptions;

namespace EKundalik.Services.StudentTeachers
{
    public partial class StudentTeacherService
    {
        private static void ValidateStudentTeacher(StudentTeacher studentTeacher)
        {
            ValidateStudentTeacherIsNotNull(studentTeacher);

            Validate(
                (Rule: IsInvalid(studentTeacher.Id), Parameter: nameof(StudentTeacher.Id)),
                (Rule: IsInvalid(studentTeacher.StudentId), Parameter: nameof(StudentTeacher.StudentId)),
                (Rule: IsInvalid(studentTeacher.TeacherId), Parameter: nameof(StudentTeacher.TeacherId)),
                (Rule: IsInvalid(studentTeacher.SubjectId), Parameter: nameof(StudentTeacher.SubjectId))
                );
        }

        private static void ValidateStudentTeacherOnModify(StudentTeacher studentTeacher)
        {
            ValidateStudentTeacherIsNotNull(studentTeacher);

            Validate(
                (Rule: IsInvalid(studentTeacher.Id), Parameter: nameof(StudentTeacher.Id)),
                (Rule: IsInvalid(studentTeacher.StudentId), Parameter: nameof(StudentTeacher.StudentId)),
                (Rule: IsInvalid(studentTeacher.TeacherId), Parameter: nameof(StudentTeacher.TeacherId)),
                (Rule: IsInvalid(studentTeacher.SubjectId), Parameter: nameof(StudentTeacher.SubjectId))

                );
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidStudentTeacherException = new InvalidStudentTeacherException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidStudentTeacherException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidStudentTeacherException.ThrowIfContainsErrors();
        }

        private static void ValidateStorageStudentTeacher(StudentTeacher maybeStudentTeacher, Guid id)
        {
            if (maybeStudentTeacher is null)
            {
                throw new NotFoundStudentTeacherException(id);
            }
        }

        private static void ValidateStudentTeacherIsNotNull(StudentTeacher studentTeacher)
        {
            if (studentTeacher is null)
            {
                throw new NullStudentTeacherException();
            }
        }

        private static void ValidateStudentTeacherId(Guid id)
        {
            Validate((Rule: IsInvalid(id), Parameter: nameof(StudentTeacher.Id)));
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == default,
            Message = "Id is required"
        };
    }
}
