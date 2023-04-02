// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
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

        public async ValueTask<T> SelectByIdAsync<T>(Guid id, string tableName, string idColumnName = "id")
        {
            await using (var connection = new NpgsqlConnection(this.connectionString))
            {
                string query = $"select * from {tableName} where {idColumnName} = @Id";

                var isHave = await connection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });

                return isHave;
            }
        }

        public IQueryable<T> SelectAll<T>(string tableName)
        {
            using (var connection = new NpgsqlConnection(this.connectionString))
            {
                string query = $"select * from {tableName}";
                IQueryable<T> item = connection.Query<T>(query).AsQueryable<T>();

                return item;
            }
        }

        public async ValueTask<T> UpdateAsync<T>(T @object, string tableName)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                string sqlQuery = $"UPDATE {tableName} SET ";
                var propertyNames = typeof(T).GetProperties().Where(p => p.Name != "Id");

                foreach (var property in propertyNames)
                {
                    sqlQuery += $"{property.Name} = @{property.Name}, ";
                }
                sqlQuery = sqlQuery.Remove(sqlQuery.Length - 2);
                sqlQuery += " WHERE Id = @Id";

                int rowsAffected = db.Execute(sqlQuery, @object);

                return rowsAffected > 0 ? @object : default;
            }
        }

        public async ValueTask<int> DeleteAsync<T>(Guid id, string idColumnName = "id")
        {
            string tableName = typeof(T).Name + "s";

            await using (var connection = new NpgsqlConnection(this.connectionString))
            {
                string query = $"DELETE FROM {tableName} WHERE {idColumnName} = @Id";

                return await connection.ExecuteAsync(query, new { Id = id });
            }
        }

        public async ValueTask<T> SelectObjectByUserName<T>(string name, string tableName, string column = "username")
        {
            using (var connection = new NpgsqlConnection(this.connectionString))
            {
                string query = $"SELECT * FROM {tableName} WHERE {column} = @UserName";

                T isHave = await connection.QueryFirstOrDefaultAsync<T>(query, new { UserName = name.ToLower() });

                return isHave;
            }
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
