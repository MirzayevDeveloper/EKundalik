// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using Xeptions;

namespace EKundalik.Models.Teachers.Exceptions
{
    public class InvalidTeacherException : Xeption
    {
        public InvalidTeacherException()
            : base(message: "Teacher is invalid")
        { }
    }
}
