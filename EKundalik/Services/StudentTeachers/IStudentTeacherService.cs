// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EKundalik.Models.StudentTeachers;

namespace EKundalik.Services.StudentTeachers
{
    public interface IStudentTeacherService
    {
        ValueTask<StudentTeacher> AddStudentTeacherAsync(StudentTeacher studentTeacher);
        ValueTask<StudentTeacher> RetrieveStudentTeacherIdAsync(Guid studentTeacherId);
        IQueryable<StudentTeacher> RetrieveAllStudentTeachers();
        ValueTask<StudentTeacher> ModifyStudentTeacherAsync(StudentTeacher studentTeacher);
        ValueTask<StudentTeacher> RemoveStudentTeacherByIdAsync(Guid studentTeacherId);
    }
}
