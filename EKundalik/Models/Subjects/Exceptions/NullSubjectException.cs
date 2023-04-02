// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using Xeptions;

namespace EKundalik.Models.Subjects.Exceptions
{
    public class NullSubjectException : Xeption
    {
        public NullSubjectException()
            : base("Subject is null.") { }
    }
}
