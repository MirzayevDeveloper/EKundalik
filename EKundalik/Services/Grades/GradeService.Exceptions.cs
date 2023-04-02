// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using EKundalik.Models.Grades.Exceptions;
using EKundalik.Models.Grades;
using System.Threading.Tasks;
using System;

namespace EKundalik.Services.Grades
{
    public partial class GradeService
    {
        private delegate ValueTask<Grade> ReturningGradeFunction();

        private async ValueTask<Grade> TryCatch(ReturningGradeFunction returningGradeFunction)
        {
            try
            {
                return await returningGradeFunction.Invoke();
            }
            catch (InvalidGradeException invalidGradeException)
            {
                foreach (var item in invalidGradeException.Data.Keys)
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
