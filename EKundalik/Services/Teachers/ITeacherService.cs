// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EKundalik.Models.Teachers;

namespace EKundalik.Services.Teachers
{
    public interface ITeacherService
    {
        ValueTask<Teacher> AddTeacherAsync(Teacher Teacher);
        ValueTask<Teacher> RetrieveTeacherByIdAsync(Guid TeacherId);
        ValueTask<Teacher> RetrieveTeacherByUserName(string userName);
        IQueryable<Teacher> RetrieveAllTeachers();
        ValueTask<Teacher> ModifyTeacherAsync(Teacher Teacher);
        ValueTask<Teacher> RemoveTeacherByIdAsync(Guid TeacherId);
    }
}
