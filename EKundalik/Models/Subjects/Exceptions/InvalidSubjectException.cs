// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using Xeptions;

namespace EKundalik.Models.Subjects.Exceptions
{
    public class InvalidSubjectException : Xeption
    {
        public InvalidSubjectException()
            : base(message: "Subject is invalid")
        { }
    }
}
