// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using Xeptions;

namespace EKundalik.Models.Grades.Exceptions
{
    public class NullGradeException : Xeption
    {
        public NullGradeException()
            : base("Grade is null.") { }
    }
}
