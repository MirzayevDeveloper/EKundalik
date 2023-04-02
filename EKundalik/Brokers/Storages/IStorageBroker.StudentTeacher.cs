// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using EKundalik.Models.StudentTeachers;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace EKundalik.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<StudentTeacher> InsertStudentTeacherAsync(StudentTeacher StudentTeacher);
        ValueTask<StudentTeacher> SelectStudentTeacherByIdAsync(Guid id);
        IQueryable<StudentTeacher> SelectAllStudentTeachers();
        ValueTask<StudentTeacher> UpdateStudentTeacherAsync(StudentTeacher StudentTeacher);
        ValueTask<StudentTeacher> DeleteStudentTeacherAsync(StudentTeacher StudentTeacher);
    }
}
