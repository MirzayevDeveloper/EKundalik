using System;
using System.Linq;
using System.Threading.Tasks;
using EKundalik.Models.Subjects;

namespace EKundalik.Brokers.Storages
{
    public partial class StorageBroker
    {
        private const string subjectTable = "Subjects";
        public async ValueTask<Subject> InsertSubjectAsync(Subject Subject)
        {
            string columns = "id, subjectname";
            string values = "@Id, @SubjectName";
            Subject.SubjectName = Subject.SubjectName.ToLower();

            return await this.InsertAsync(Subject, subjectTable, (columns, values));
        }

        public async ValueTask<Subject> SelectSubjectByIdAsync(Guid id) =>
            await this.SelectByIdAsync<Subject>(id, subjectTable);

        public async ValueTask<Subject> SelectSubjectBySubjectNameAsync(string SubjectName) =>
            await SelectObjectByUserName<Subject>(SubjectName, subjectTable);

        public IQueryable<Subject> SelectAllSubjects() =>
            SelectAll<Subject>(subjectTable);

        public async ValueTask<Subject> UpdateSubjectAsync(Subject Subject) =>
            await UpdateAsync(Subject, subjectTable);

        public async ValueTask<Subject> DeleteSubjectAsync(Subject Subject)
        {
            await DeleteAsync<Subject>(Subject.Id);

            return Subject;
        }
    }
}
