// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;

namespace EKundalik.Models.Grades
{
    public class Grade
    {
        public Guid Id { get; set; }
        public GradeEnum GradeRate { get; set; }
        public DateTime Date { get; set; }
        public Guid StudentTeacherId { get; set; }
    }
}
