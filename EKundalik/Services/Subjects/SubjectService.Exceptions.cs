// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Threading.Tasks;
using EKundalik.Models.Subjects;
using EKundalik.Models.Subjects.Exceptions;

namespace EKundalik.Services.Subjects
{
    public partial class SubjectService
    {
        private delegate ValueTask<Subject> ReturningSubjectFunction();

        private async ValueTask<Subject> TryCatch(ReturningSubjectFunction returningSubjectFunction)
        {
            try
            {
                return await returningSubjectFunction.Invoke();
            }
            catch (InvalidSubjectException invalidSubjectException)
            {
                foreach (var item in invalidSubjectException.Data.Keys)
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
