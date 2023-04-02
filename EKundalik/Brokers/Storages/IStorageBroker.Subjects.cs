// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EKundalik.Models.Subjects;

namespace EKundalik.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Subject> InsertSubjectAsync(Subject Subject);
        ValueTask<Subject> SelectSubjectByIdAsync(Guid id);
        ValueTask<Subject> SelectSubjectBySubjectNameAsync(string subjectName);
        IQueryable<Subject> SelectAllSubjects();
        ValueTask<Subject> UpdateSubjectAsync(Subject Subject);
        ValueTask<Subject> DeleteSubjectAsync(Subject Subject);
    }
}
