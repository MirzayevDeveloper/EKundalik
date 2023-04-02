// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EKundalik.Models.Teachers;

namespace EKundalik.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Teacher> InsertTeacherAsync(Teacher Teacher);
        ValueTask<Teacher> SelectTeacherByIdAsync(Guid id);
        ValueTask<Teacher> SelectTeacherByUserNameAsync(string userName);
        IQueryable<Teacher> SelectAllTeachers();
        ValueTask<Teacher> UpdateTeacherAsync(Teacher Teacher);
        ValueTask<Teacher> DeleteTeacherAsync(Teacher Teacher);
    }
}
