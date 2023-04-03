// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using EKundalik.Models.StudentTeachers;
using System.Linq;
using System.Threading.Tasks;
using System;
using EKundalik.Models.Students;
using EKundalik.Models.Teachers;
using EKundalik.Models.Subjects;

namespace EKundalik.Brokers.Storages
{
    public partial class StorageBroker
    {
        private const string studentTeacherTabel = "studentsteachers";
        public async ValueTask<StudentTeacher> InsertStudentTeacherAsync(StudentTeacher studentTeacher)
        {
            string columns = "id, studentid, teacherid, subjectid";
            string values = "@Id, @StudentId, @TeacherId, @SubjectId";

            var @object = new 
            { 
                studentTeacher.Id,
                studentTeacher.StudentId,
                studentTeacher.TeacherId,
                studentTeacher.SubjectId
            };

            var studentTeacher1 =  await this.InsertAsync(@object, studentTeacherTabel, (columns, values));

            return new StudentTeacher()
            {
                Id = studentTeacher1.Id,
                StudentId = studentTeacher1.StudentId,
                TeacherId = studentTeacher1.TeacherId,
                SubjectId = studentTeacher1.SubjectId
            };
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
