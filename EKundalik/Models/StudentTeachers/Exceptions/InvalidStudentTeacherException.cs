// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using Xeptions;

namespace EKundalik.Models.StudentTeachers.Exceptions
{
    public class InvalidStudentTeacherException : Xeption
    {
        public InvalidStudentTeacherException()
            : base(message: "StudentTeacher is invalid")
        { }
    }
}
