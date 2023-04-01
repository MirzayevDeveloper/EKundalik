// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System.Threading.Tasks;
using EKundalik.Models.Teachers;

namespace EKundalik.Services.Teachers
{
    public interface ITeacherService
    {
        ValueTask<Teacher> AddTeacherAsync(Teacher teacher);
    }
}
