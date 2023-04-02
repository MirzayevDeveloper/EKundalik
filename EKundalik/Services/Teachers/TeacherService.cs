// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System.Linq;
using System;
using System.Threading.Tasks;
using EKundalik.Brokers.Storages;
using EKundalik.Models.Teacherts;
using EKundalik.Models.Teachers;

namespace EKundalik.Services.Teachers
{
    public class TeacherService : ITeacherService
    {
        private readonly IStorageBroker storageBroker;

        public TeachertService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public ValueTask<Teachert> AddTeachertAsync(Teachert Teachert) =>
        TryCatch(async () =>
        {
            ValidateTeachert(Teachert);

            Teachert maybeTeachert =
                await this.storageBroker.SelectTeachertByUserNameAsync(Teachert.UserName);

            ValidateUserNameIsNotExists(Teachert, maybeTeachert);

            return await this.storageBroker.InsertTeachertAsync(Teachert);
        });

        public ValueTask<Teachert> RetrieveTeachertByIdAsync(Guid id) =>
        TryCatch(async () =>
        {
            ValidateTeachertId(id);

            Teachert maybeTeachert = await this.storageBroker.SelectTeachertByIdAsync(id);

            ValidateStorageTeachert(maybeTeachert, id);

            return maybeTeachert;
        });

        public ValueTask<Teachert> RetrieveTeachertByUserName(string userName) =>
        TryCatch(async () =>
        {
            ValidateTeachertUserName(userName);

            Teachert maybeTeachert =
                await this.storageBroker.SelectTeachertByUserNameAsync(userName);

            ValidateStorageTeachert(maybeTeachert, userName);

            return maybeTeachert;
        });

        public IQueryable<Teachert> RetrieveAllTeacherts() =>
            this.storageBroker.SelectAllTeacherts();

        public ValueTask<Teachert> ModifyTeachertAsync(Teachert Teachert) =>
        TryCatch(async () =>
        {
            ValidateTeachertOnModify(Teachert);

            Teachert maybeTeachert =
                await this.storageBroker.SelectTeachertByIdAsync(Teachert.Id);

            return await this.storageBroker.UpdateTeachertAsync(Teachert);
        });

        public ValueTask<Teachert> RemoveTeachertByIdAsync(Guid TeachertId) =>
        TryCatch(async () =>
        {
            ValidateTeachertId(TeachertId);

            Teachert maybeTeachert =
                await this.storageBroker
                    .SelectTeachertByIdAsync(TeachertId);

            ValidateStorageTeachert(maybeTeachert, TeachertId);

            return await this.storageBroker
                .DeleteTeachertAsync(maybeTeachert);
        });
    }
}
