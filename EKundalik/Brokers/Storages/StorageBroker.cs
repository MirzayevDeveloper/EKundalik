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

        public IQueryable<T> SelectAll<T>(string tableName)
        {
            throw new NotImplementedException();
        }

        public ValueTask<T> SelectByIdAsync<T>(Guid id, string tableName, string idColumnName = "Id")
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


        public void CreateTable<T>()
        {
            var tableName = GetTableName<T>();
            var columns = GetColumn<T>();

            using (IDbConnection connection = new NpgsqlConnection(this.connectionString))
            {
                connection.Execute($"CREATE TABLE IF NOT EXISTS {tableName} ({columns});");
            }
        }
        private string GetColumn<T>()
        {
            var properties = typeof(T).GetProperties();
            var column = new List<string>();

            foreach (var property in properties)
            {
                var columnName = property.Name.ToLower();
                var columnType = GetColumnType(property.PropertyType);

                var columnDef = $"{columnName} {columnType}";
                column.Add(columnDef);
            }

            return string.Join(",", column);
        }
        private string GetTableName<T>()
        {
            return typeof(T).Name.ToLower() + "s";
        }
        private string GetColumns<T>()
        {
            return string.Join(",", typeof(T).GetProperties().Select(p => p.Name));
        }
        private string GetValues<T>()
        {
            return string.Join(",", typeof(T).GetProperties().Select(p => $"@{p.Name}"));
        }
        private string GetColumnType(Type propertyType)
        {
            if (propertyType == typeof(int))
            {
                return "integer";
            }
            else if (propertyType == typeof(string))
            {
                return "text";
            }
            else if (propertyType == typeof(DateTime))
            {
                return "timestamp";
            }
            else if (propertyType == typeof(bool))
            {
                return "boolean";
            }
            else if (propertyType == typeof(Guid))
            {
                return "uuid";
            }

            throw new ArgumentException($"Unsupported type: {propertyType.Name}");
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
