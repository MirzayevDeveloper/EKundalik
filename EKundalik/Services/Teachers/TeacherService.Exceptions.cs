// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Threading.Tasks;
using EKundalik.Models.Teachers;
using EKundalik.Models.Teachers.Exceptions;

namespace EKundalik.Services.Teachers
{
    public partial class TeacherService
    {
        private delegate ValueTask<Teacher> ReturningTeacherFunction();

        private async ValueTask<Teacher> TryCatch(ReturningTeacherFunction returningTeacherFunction)
        {
            try
            {
                return await returningTeacherFunction.Invoke();
            }
            catch (InvalidTeacherException invalidTeacherException)
            {
                foreach (var item in invalidTeacherException.Data.Keys)
                    Console.WriteLine("\nInvalid " + item);

                return null;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return null;
            }
        }
    }
}
