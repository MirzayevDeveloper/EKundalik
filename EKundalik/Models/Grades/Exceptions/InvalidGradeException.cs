// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using Xeptions;

namespace EKundalik.Models.Grades.Exceptions
{
    public class InvalidGradeException : Xeption
    {
        public InvalidGradeException()
            : base(message: "Grade is invalid")
        { }
    }
}
