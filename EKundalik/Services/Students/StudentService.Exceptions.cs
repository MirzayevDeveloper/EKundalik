// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Threading.Tasks;
using EKundalik.Models.Students;
using EKundalik.Models.Students.Exceptions;

namespace EKundalik.Services.Students
{
    public partial class StudentService
    {
        private delegate ValueTask<Student> ReturningStudentFunction();

        private async ValueTask<Student> TryCatch(ReturningStudentFunction returningStudentFunction)
        {
            try
            {
                return await returningStudentFunction.Invoke();
            }
            catch (InvalidStudentException invalidStudentException)
            {
                foreach (var item in invalidStudentException.Data.Keys)
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
