using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using LiquibaseEditor.Constants;
using LiquibaseEditor.UnitOfWork;

namespace LiquibaseEditor.Infrastructure.SqlServer.UnitOfWork
{
    public class UnitOfWorkSqlServerDatabase : IUnitOfWork
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;
        private readonly bool _transactionHasOpen;
        private bool _disposed;

        public UnitOfWorkSqlServerDatabase(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction(IsolationLevel.ReadCommitted);
            _transactionHasOpen = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public string Database => Databases.SqlServer;

        public bool IsConnected()
        {
            return _connection.State == ConnectionState.Open;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_transactionHasOpen)
                    _transaction.Rollback();

                _connection.Close();
                _connection.Dispose();
            }

            _disposed = true;
        }

        public List<T> Query<T>(string query)
        {
            return _connection.Query<T>(query, null, _transaction).ToList();
        }

        public List<T> Query<T>(string query, object param)
        {
            return _connection.Query<T>(query, param, _transaction).ToList();
        }
    }
}