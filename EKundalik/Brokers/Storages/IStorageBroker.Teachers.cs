﻿// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System.Threading.Tasks;
using EKundalik.Models.Teachers;

namespace EKundalik.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Teacher> InsertTeacherAsync(Teacher teacher);
    }
}