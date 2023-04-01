// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using EKundalik.Models.Students;
using EKundalik.Models.Subjects;
using EKundalik.Models.Teachers;

namespace EKundalik.Models.StudentTeachers
{
    public class StudentTeacher
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid TeacherId { get; set; }
        public Guid SubjectId { get; set; }
        public Student Student { get; set; }
        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
    }
}
