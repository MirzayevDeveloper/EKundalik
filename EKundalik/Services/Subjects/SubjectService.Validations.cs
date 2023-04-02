// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using EKundalik.Models.Students.Exceptions;
using EKundalik.Models.Subjects;
using EKundalik.Models.Subjects.Exceptions;

namespace EKundalik.Services.Subjects
{
    public partial class SubjectService
    {
        private static void ValidateSubject(Subject Subject)
        {
            ValidateSubjectIsNotNull(Subject);

            Validate(
                (Rule: IsInvalid(Subject.Id), Parameter: nameof(Subject.Id)),
                (Rule: IsInvalid(Subject.SubjectName), Parameter: nameof(Subject.SubjectName))
                );
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidSubjectException = new InvalidSubjectException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidSubjectException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidSubjectException.ThrowIfContainsErrors();
        }

        private static void ValidateSubjectIsNotExists(Subject newSubject, Subject maybeSubject)
        {
            if (maybeSubject is null) return;

            if (newSubject.Id == maybeSubject.Id || newSubject.SubjectName.ToLower() == maybeSubject.SubjectName)
            {
                throw new AlreadyExistsStudentException(newSubject.SubjectName);
            }
        }

        private static void ValidateStorageSubject(Subject maybeSubject, string subjectName)
        {
            if (maybeSubject is null)
            {
                throw new NotFoundSubjectException(subjectName);
            }
        }

        private static void ValidateStorageSubject(Subject maybeSubject, Guid subjectId)
        {
            if (maybeSubject is null)
            {
                throw new NotFoundSubjectException(subjectId);
            }
        }

        private static void ValidateSubjectIsNotNull(Subject Subject)
        {
            if (Subject is null)
            {
                throw new NullSubjectException();
            }
        }

        private void ValidateSubjectName(string subjectName)
        {
            Validate((Rule: IsInvalid(subjectName), Parameter: nameof(Subject.SubjectName)));
        }

        private static void ValidateSubjectId(Guid id)
        {
            Validate((Rule: IsInvalid(id), Parameter: nameof(Subject.Id)));
        }

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
