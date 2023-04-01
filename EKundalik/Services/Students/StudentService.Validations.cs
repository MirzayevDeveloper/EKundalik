// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using EKundalik.Models.Students;
using EKundalik.Models.Students.Exceptions;

namespace EKundalik.Services.Students
{
    public partial class StudentService
    {
        public static void ValidateStudentOnAdd(Student student)
        {
            ValidateStudentIsNotNull(student);

            Validate(
                (Rule: IsInvalid(student.Id), Parameter: nameof(Student.Id)),
                (Rule: IsInvalid(student.FullName), Parameter: nameof(Student.FullName)),
                (Rule: IsInvalid(student.BirthDate), Parameter: nameof(Student.BirthDate))
                );
        }

        private static dynamic IsInvalid(DateTime dates) => new
        {
            Condition = dates == default,
            Message = "Date is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == default,
            Message = "Id is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidStudentException = new InvalidStudentException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidStudentException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidStudentException.ThrowIfContainsErrors();
        }

        private static void ValidateUserNameIsNotExists(Student newStudent, Student maybeStudent)
        {
            if(newStudent.Id == maybeStudent.Id || newStudent.UserName == maybeStudent.UserName)
            {
                throw new AlreadyExistsStudentException();
            }
        }

        private static void ValidateStudentIsNotNull(Student student)
        {
            if (student is null)
            {
                throw new NullStudentException();
            }
        }
    }
}
