// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using Xeptions;

namespace EKundalik.Models.Subjects.Exceptions
{
    public class AlreadyExistsSubjectException : Xeption
    {
        public AlreadyExistsSubjectException(string userName)
            : base($"\nAlready exist Subject with user name: {userName}")
        { }
    }
}
