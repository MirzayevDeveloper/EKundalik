// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using Xeptions;

namespace EKundalik.Models.Teachers.Exceptions
{
    public class NullTeacherException : Xeption
    {
        public NullTeacherException()
            : base("Teacher is null.") { }
    }
}
