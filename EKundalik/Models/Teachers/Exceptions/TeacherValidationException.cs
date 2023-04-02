// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using Xeptions;

namespace EKundalik.Models.Teachers.Exceptions
{
    public class TeacherValidationException : Xeption
    {
        public TeacherValidationException()
            : base(message: "Teacher validation error occurred, fix the errors and try again")
        { }
    }
}
