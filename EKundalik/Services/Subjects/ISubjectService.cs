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
        ValueTask<Subject> AddSubjectAsync(Subject subject);
        ValueTask<Subject> RetrieveSubjectByIdAsync(Guid subjectId);
        ValueTask<Subject> RetrieveSubjectBySubjectName(string subjectName);
        IQueryable<Subject> RetrieveAllSubjects();
        ValueTask<Subject> ModifySubjectAsync(Subject subject);
        ValueTask<Subject> RemoveSubjectByIdAsync(Guid subjectId);
    }
}
