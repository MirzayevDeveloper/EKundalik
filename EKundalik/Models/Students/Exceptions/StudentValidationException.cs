// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using Xeptions;

namespace EKundalik.Models.Students.Exceptions
{
    public class StudentValidationException : Xeption
    {
        public StudentValidationException()
            : base(message: "Student validation error occurred, fix the errors and try again")
        { }
    }
}
