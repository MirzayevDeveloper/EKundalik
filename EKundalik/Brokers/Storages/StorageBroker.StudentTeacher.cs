// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using EKundalik.Models.StudentTeachers;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace EKundalik.Brokers.Storages
{
    public partial class StorageBroker
    {
        private const string studentTeacherTabel = "studentsteachers";
        public async ValueTask<StudentTeacher> InsertStudentTeacherAsync(StudentTeacher studentTeacher)
        {
            string columns = "id, studentid, teacherid, subjectid";
            string values = "@Id, @StudentId, @TeacherId, @SubjectId";

            return await this.InsertAsync(studentTeacher, studentTeacherTabel, (columns, values));
        }

        public async ValueTask<StudentTeacher> SelectStudentTeacherByIdAsync(Guid id) =>
            await this.SelectByIdAsync<StudentTeacher>(id, studentTeacherTabel);

        public IQueryable<StudentTeacher> SelectAllStudentTeachers() =>
            SelectAll<StudentTeacher>(studentTeacherTabel);

        public async ValueTask<StudentTeacher> UpdateStudentTeacherAsync(StudentTeacher studentTeacher) =>
            await UpdateAsync(studentTeacher, studentTeacherTabel);

        public async ValueTask<StudentTeacher> DeleteStudentTeacherAsync(StudentTeacher studentTeacher)
        {
            await DeleteAsync<StudentTeacher>(studentTeacher.Id);

            return studentTeacher;
        }
    }
}
