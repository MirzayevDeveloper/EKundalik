// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using Xeptions;

namespace EKundalik.Models.StudentTeachers.Exceptions
{
    public class NotFoundStudentTeacherException : Xeption
    {
        public NotFoundStudentTeacherException(Guid id)
            : base($"Could not find StudentTeacher with id: {id}")
        { }

        public NotFoundStudentTeacherException(string userName)
            : base($"Could not find StudentTeacher with username: {userName}")
        { }

        public NotFoundStudentTeacherException()
            : base("Could not find user")
        { }
    }
}
