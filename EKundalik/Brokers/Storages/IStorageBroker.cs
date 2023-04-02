// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;

namespace EKundalik.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<T> InsertAsync<T>(T @object, string tableName, (string column, string value) values);
        ValueTask<T> SelectByIdAsync<T>(Guid id, string tableName, string idColumnName = "id");
        IQueryable<T> SelectAll<T>(string tableName);
        ValueTask<T> UpdateAsync<T>(T @object, string tableName);
        ValueTask<int> DeleteAsync<T>(Guid id, string idColumnName = "id");
        ValueTask<T> SelectObjectByUserName<T>(string name, string tableName, string column = "username");
    }
}
