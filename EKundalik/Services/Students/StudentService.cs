// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System.Threading.Tasks;
using EKundalik.Brokers.Storages;
using EKundalik.Models.Students;

namespace EKundalik.Services.Students
{
    public partial class StudentService : IStudentService
    {
        private readonly IStorageBroker storageBroker;

        public StudentService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public ValueTask<Student> AddStudentAsync(Student student)
        {
            ValidateStudentOnAdd(student);

            return this.storageBroker.InsertStudentAsync(student);
        }
    }
}
