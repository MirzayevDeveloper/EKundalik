// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using EKundalik.Models.StudentTeachers.Exceptions;
using EKundalik.Models.StudentTeachers;
using System.Threading.Tasks;
using System;

namespace EKundalik.Services.StudentTeachers
{
    public partial class StudentTeacherService
    {

        private delegate ValueTask<StudentTeacher> ReturningStudentTeacherFunction();

        private async ValueTask<StudentTeacher> TryCatch(ReturningStudentTeacherFunction returningStudentTeacherFunction)
        {
            try
            {
                return await returningStudentTeacherFunction.Invoke();
            }
            catch (InvalidStudentTeacherException invalidStudentTeacherException)
            {
                foreach (var item in invalidStudentTeacherException.Data.Keys)
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
