// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EKundalik.Models.Students;

namespace EKundalik.Services.Students
{
    public interface IStudentService
    {
        ValueTask<Student> AddStudentAsync(Student student);
        ValueTask<Student> RetrieveStudentByIdAsync(Guid studentId);
        ValueTask<Student> RetrieveStudentByUserName(string userName);
        IQueryable<Student> RetrieveAllStudents();
        ValueTask<Student> ModifyStudentAsync(Student student);
    }
}
