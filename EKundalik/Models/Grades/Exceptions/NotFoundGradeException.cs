// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using Xeptions;

namespace EKundalik.Models.Grades.Exceptions
{
    public class NotFoundGradeException : Xeption
    {
        public NotFoundGradeException(Guid id)
            : base($"Could not find Grade with id: {id}")
        { }

        public NotFoundGradeException(string userName)
            : base($"Could not find Grade with username: {userName}")
        { }

        public NotFoundGradeException()
            : base("Could not find user")
        { }
    }
}
