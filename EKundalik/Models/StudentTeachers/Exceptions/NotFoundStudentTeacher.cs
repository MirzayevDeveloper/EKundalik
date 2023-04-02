// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using Xeptions;

namespace EKundalik.Models.StudentTeacherTeachers.Exceptions
{
    public class NotFoundStudentTeacherTeacher : Xeption
    {
        public NotFoundStudentTeacherTeacher(Guid id)
            : base($"Not found with id: {id}") { }
    }
}
