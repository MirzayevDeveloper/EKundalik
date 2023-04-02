// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using Xeptions;

namespace EKundalik.Models.Students.Exceptions
{
    public class NotFoundStudentException : Xeption
    {
        public NotFoundStudentException(Guid id)
            : base($"Could not find Student with id: {id}")
        { }

        public NotFoundStudentException(string userName)
            : base($"Could not find Student with username: {userName}")
        { }

        public NotFoundStudentException()
            : base("Could not find user")
        { }
    }
}
