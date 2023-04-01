// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using Xeptions;

namespace EKundalik.Models.Students.Exceptions
{
    public class NotFoundStudentException : Xeption
    {
        public NotFoundStudentException(string userName)
            : base($"Could not find Student with username: {userName}") 
        { }

        public NotFoundStudentException()
            : base("Could not find user")
        { }
    }
}
