using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outrage.Tenancy.Postgres
{
    internal class PostgresDatabaseWrapper : IDatabaseWrapper, IDisposable
    {
        private readonly string connectionString;
        private IDbConnection postgresConnection;

        public PostgresDatabaseWrapper(string connectionString)
        {
            this.connectionString = connectionString;
            this.postgresConnection = CreateConnection();
        }

        private IDbConnection CreateConnection()
        {
            if (postgresConnection == null)
            {
                postgresConnection = new Npgsql.NpgsqlConnection(connectionString);
                postgresConnection.Open();
            }
            return postgresConnection;
        }

        public bool EstablishDatabase(string databaseName, string username, string password)
        {
            if (CreateDatabase(databaseName))
            {
                CreateUser(username, password);
                GrantDatabase(username, databaseName);
                AlterTableOwner(username, databaseName);

                return true;
            }

            return false;
        }

        public bool CreateDatabase(string databaseName)
        {
            var sqlDatabaseExists = $"select exists (select 1 from pg_database where datname = '{databaseName}');";
            using var databaseExistsCommand = postgresConnection.CreateCommand();
            databaseExistsCommand.CommandText = sqlDatabaseExists;
            var databaseExists = (bool)(databaseExistsCommand.ExecuteScalar() ?? false);

            if (!databaseExists)
            {
                var sqlCreateDatabase = $"create database \"{databaseName}\";";
                using var createDatabaseCommand = postgresConnection.CreateCommand();
                createDatabaseCommand.CommandText = sqlCreateDatabase;
                createDatabaseCommand.ExecuteNonQuery();

                return true;
            }
            else
                return false;
        }

        public bool CreateUser(string username, string password)
        {
            bool userExists = false;
            var connection = CreateConnection();
            var sqlCheckUser = $"select exists (select 1 from pg_roles where rolname = '{username}');";
            using (var checkUserCommand = connection.CreateCommand())
            {
                checkUserCommand.CommandText = sqlCheckUser;
                userExists = (bool)(checkUserCommand.ExecuteScalar() ?? false);
            }
            if (!userExists)
            {
                var sqlCreateUser = $"create user {username} with password '{password}';";
                using (var createUserCommand = connection.CreateCommand())
                {
                    createUserCommand.CommandText = sqlCreateUser;
                    return createUserCommand.ExecuteNonQuery() >= 0;
                }
            }
            else
                return false;
        }

        public bool GrantDatabase(string username, string database, string privileges = "all")
        {
            var sqlGrant = $"grant {privileges} privileges on database {database} to {username};";
            using var grantUserCommand = postgresConnection.CreateCommand();
            grantUserCommand.CommandText = sqlGrant;
            return grantUserCommand.ExecuteNonQuery() >= 1;
        }

        public bool AlterTableOwner(string username, string database)
        {
            var sqlChown = $"alter database {database} owner to {username}";
            using var alterDatabaseOwnerCommand = postgresConnection.CreateCommand();
            alterDatabaseOwnerCommand.CommandText = sqlChown;
            return alterDatabaseOwnerCommand.ExecuteNonQuery() >= 1;
        }

        public void Dispose()
        {
            this.postgresConnection?.Dispose();
        }
    }
}
