// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using EKundalik.Models.Students;
using Microsoft.Extensions.Configuration;
using Npgsql;
using static Dapper.SqlMapper;

namespace EKundalik.Brokers.Storages
{
    public partial class StorageBroker : IStorageBroker
    {
        private readonly IConfiguration configuration;
        private readonly string databaseName, connectionString;
        public StorageBroker()
        {
            this.configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory() + "..\\..\\..\\..\\")
                .AddJsonFile("appsettings.json", optional: true).Build();

            databaseName = new NpgsqlConnectionStringBuilder(
                this.configuration.GetConnectionString("DefaultConnection")).Database;

            connectionString = this.configuration
                .GetConnectionString("DefaultConnection");

            CreateDatabaseIfNotExists(connectionString, databaseName);
        }

        public async ValueTask<T> InsertAsync<T>(T @object, string tableName, (string column, string value) values)
        {
            await using (var connection = new NpgsqlConnection(this.connectionString))
            {
                connection.Open();

                var query = $"INSERT INTO {tableName} ({values.column}) VALUES ({values.value})";

                connection.Execute(query, @object);

                return @object;
            }
        }


        public async ValueTask<T> SelectByIdAsync<T>(Guid id, string tableName, string idColumnName = "id")
        {
            await using (var connection = new NpgsqlConnection(this.connectionString))
            {
                string query = $"select * from {tableName} where {idColumnName} = @{id}";

                var isHave = connection.QueryFirstOrDefault<T>(query, new { Id = id });

                return isHave;
            }
        }

        public IQueryable<T> SelectAll<T>(string tableName)
        {
            throw new NotImplementedException();
        }

        public ValueTask<T> UpdateAsync<T>(T @object, string tableName)
        {
            throw new NotImplementedException();
        }

        public ValueTask<T> DeleteAsync<T>(T @object, string tableName)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<T> SelectObjectByUserName<T>(string userName, string tableName)
        {

        }

        private string GetTableName<T>()
        {
            return typeof(T).Name.ToLower() + "s";
        }
        private void CreateDatabaseIfNotExists(string connectionString, string databaseName)
        {
            using (var connection = new NpgsqlConnection(this.configuration.GetConnectionString("Postgres")))
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;

                    command.CommandText = $"SELECT 1 FROM pg_database where datname = '{databaseName.ToLower()}'";

                    var exists = command.ExecuteScalar() != null;

                    if (!exists)
                    {
                        command.CommandText = $"create database {databaseName}";
                        command.ExecuteNonQuery();
                        CreateTablesIfNorExists();
                    }
                }
            }
        }
        private void CreateTablesIfNorExists()
        {
            using (var connection = new NpgsqlConnection(this.connectionString))
            {
                connection.Open();

                string query = $@"{File.ReadAllText("../../../query.txt")}";
                connection.Execute(query);
            }
        }
    }
}
