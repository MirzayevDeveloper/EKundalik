// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using Xeptions;

namespace EKundalik.Models.Subjects.Exceptions
{
    public class NotFoundSubjectException : Xeption
    {
        public NotFoundSubjectException(Guid id)
            : base($"Could not find Subject with id: {id}")
        { }

        public NotFoundSubjectException(string userName)
            : base($"Could not find Subject with username: {userName}")
        { }

        public NotFoundSubjectException()
            : base("Could not find user")
        { }
    }
}
