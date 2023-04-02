// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using Xeptions;

namespace EKundalik.Models.Teachers.Exceptions
{
    public class NotFoundTeacherException : Xeption
    {
        public NotFoundTeacherException(Guid id)
            : base($"Could not find Teacher with id: {id}")
        { }

        public NotFoundTeacherException(string userName)
            : base($"Could not find Teacher with username: {userName}")
        { }

        public NotFoundTeacherException()
            : base("Could not find user")
        { }
    }
}
