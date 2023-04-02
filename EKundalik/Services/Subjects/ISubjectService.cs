// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EKundalik.Models.Subjects;

namespace EKundalik.Services.Subjects
{
    public interface ISubjectService
    {
        ValueTask<Subject> AddSubjectAsync(Subject Subject);
        ValueTask<Subject> RetrieveSubjectByIdAsync(Guid SubjectId);
        ValueTask<Subject> RetrieveSubjectBySubjectName(string userName);
        IQueryable<Subject> RetrieveAllSubjects();
        ValueTask<Subject> ModifySubjectAsync(Subject Subject);
        ValueTask<Subject> RemoveSubjectByIdAsync(Guid SubjectId);
    }
}
