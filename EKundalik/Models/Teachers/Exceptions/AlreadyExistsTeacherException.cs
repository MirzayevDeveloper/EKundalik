﻿// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using Xeptions;

namespace EKundalik.Models.Teachers.Exceptions
{
    public class AlreadyExistsTeacherException : Xeption
    {
        public AlreadyExistsTeacherException(string userName)
            : base($"\nAlready exist Teacher with user name: {userName}")
        { }
    }
}
