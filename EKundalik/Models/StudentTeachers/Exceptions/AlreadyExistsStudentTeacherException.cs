// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using Xeptions;

namespace EKundalik.Models.StudentTeachers.Exceptions
{
    public class AlreadyExistsStudentTeacherException : Xeption
    {
        public AlreadyExistsStudentTeacherException(string userName)
            : base($"\nAlready exist StudentTeacher with user name: {userName}")
        { }
    }
}
