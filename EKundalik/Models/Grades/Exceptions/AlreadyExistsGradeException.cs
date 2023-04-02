// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using Xeptions;

namespace EKundalik.Models.Grades.Exceptions
{
    public class AlreadyExistsGradeException : Xeption
    {
        public AlreadyExistsGradeException(string userName)
            : base($"\nAlready exist Grade with user name: {userName}")
        { }
    }
}
