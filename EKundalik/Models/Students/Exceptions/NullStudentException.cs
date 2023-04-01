// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using Xeptions;

namespace EKundalik.Models.Students.Exceptions
{
    public class NullStudentException : Xeption
    {
        public NullStudentException()
            : base("Student is null.") { }
    }
}
