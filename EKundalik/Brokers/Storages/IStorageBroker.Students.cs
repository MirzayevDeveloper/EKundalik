// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Threading.Tasks;
using EKundalik.Models.Students;

namespace EKundalik.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Student> InsertStudentAsync(Student student);
        ValueTask<Student> SelectStudentByIdAsync(Guid id);
        ValueTask<Student> SelectStudentByUserNameAsync(string userName);
    }
}
