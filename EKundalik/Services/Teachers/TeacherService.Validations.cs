// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Linq;
using EKundalik.Models.Teachers;
using EKundalik.Models.Teachers.Exceptions;

namespace EKundalik.Services.Teachers
{
    public partial class TeacherService
    {
        private static void ValidateTeacher(Teacher Teacher)
        {
            ValidateTeacherIsNotNull(Teacher);

            Validate(
                (Rule: IsInvalid(Teacher.Id), Parameter: nameof(Teacher.Id)),
                (Rule: IsInvalid(Teacher.UserName), Parameter: nameof(Teacher.UserName)),
                (Rule: IsInvalid(Teacher.FullName), Parameter: nameof(Teacher.FullName)),
                (Rule: IsInvalid(Teacher.BirthDate), Parameter: nameof(Teacher.BirthDate)),
                (Rule: IsInvalidUserName(Teacher.UserName), Parameter: nameof(Teacher.UserName)),
                (Rule: IsInvalidName(Teacher.FullName), Parameter: nameof(Teacher.FullName))
                );
        }

        private static void ValidateTeacherOnModify(Teacher Teacher)
        {
            ValidateTeacherIsNotNull(Teacher);

            Validate(
                (Rule: IsInvalid(Teacher.FullName), Parameter: nameof(Teacher.FullName)),
                (Rule: IsInvalid(Teacher.UserName), Parameter: nameof(Teacher.UserName)),
                (Rule: IsInvalid(Teacher.BirthDate), Parameter: nameof(Teacher.BirthDate)),
                (Rule: IsInvalidUserName(Teacher.UserName), Parameter: nameof(Teacher.UserName)),
                (Rule: IsInvalidName(Teacher.FullName), Parameter: nameof(Teacher.FullName))
                );
        }

        private static void ValidateUserNameIsNotExists(Teacher newTeacher, Teacher maybeTeacher)
        {
            if (maybeTeacher is null) return;

            if (newTeacher.Id == maybeTeacher.Id || newTeacher.UserName.ToLower() == maybeTeacher.UserName)
            {
                throw new AlreadyExistsTeacherException(newTeacher.UserName);
            }
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidTeacherException = new InvalidTeacherException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidTeacherException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidTeacherException.ThrowIfContainsErrors();
        }

        private static void ValidateStorageTeacher(Teacher maybeTeacher, string userName)
        {
            if (maybeTeacher is null)
            {
                throw new NotFoundTeacherException(userName);
            }
        }

        private static void ValidateStorageTeacher(Teacher maybeTeacher, Guid userId)
        {
            if (maybeTeacher is null)
            {
                throw new NotFoundTeacherException(userId);
            }
        }

        private static void ValidateTeacherIsNotNull(Teacher Teacher)
        {
            if (Teacher is null)
            {
                throw new NullTeacherException();
            }
        }

        private static void ValidateTeacherUserName(string userName)
        {
            Validate(
                (Rule: IsInvalid(userName), Parameter: nameof(Teacher.UserName)),
                (Rule: IsInvalidUserName(userName), Parameter: nameof(Teacher.UserName))
                );
        }

        private static void ValidateTeacherId(Guid id)
        {
            Validate((Rule: IsInvalid(id), Parameter: nameof(Teacher.Id)));
        }

        private static dynamic IsInvalidUserName(string text) => new
        {
            Condition = text.All(char.IsNumber),
            Message = "User name is invalid"
        };

        private static dynamic IsInvalidName(string text) => new
        {
            Condition = text.Any(char.IsNumber),
            Message = "Full name is invalid"
        };

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
    }
}
