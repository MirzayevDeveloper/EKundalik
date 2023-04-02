// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using Xeptions;

namespace EKundalik.Models.StudentTeachers.Exceptions
{
    public class NullStudentTeacherException : Xeption
    {
        public NullStudentTeacherException()
            : base("StudentTeacher is null.") { }
    }
}
