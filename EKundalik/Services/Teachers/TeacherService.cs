// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System.Threading.Tasks;
using EKundalik.Brokers.Storages;
using EKundalik.Models.Teachers;

namespace EKundalik.Services.Teachers
{
    public class TeacherService : ITeacherService
    {
        private readonly IStorageBroker storageBroker;

        public TeacherService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public ValueTask<Teacher> AddTeacherAsync(Teacher teacher) =>
            this.storageBroker.InsertTeacherAsync(teacher);
    }
}
