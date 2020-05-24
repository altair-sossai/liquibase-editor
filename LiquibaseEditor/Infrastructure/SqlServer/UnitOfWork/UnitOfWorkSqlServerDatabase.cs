using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using LiquibaseEditor.UnitOfWork;

namespace LiquibaseEditor.Infrastructure.SqlServer.UnitOfWork
{
    public class UnitOfWorkSqlServerDatabase : IUnitOfWork
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;
        private bool _disposed;
        private bool _transactionHasOpen;

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

        public string Database => "SQL Server";

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

        public void Commit()
        {
            if (!_transactionHasOpen)
                throw new InvalidOperationException();

            _transaction.Commit();
            _transactionHasOpen = false;
        }

        public void RollBack()
        {
            if (!_transactionHasOpen)
                throw new InvalidOperationException();

            _transaction.Rollback();
            _transactionHasOpen = false;
        }

        public List<T> Query<T>(string query)
        {
            return _connection.Query<T>(query, null, _transaction).ToList();
        }

        public List<T> Query<T>(string query, object param)
        {
            return _connection.Query<T>(query, param, _transaction).ToList();
        }

        public IEnumerable<dynamic> Query(string query, object param)
        {
            return _connection.Query(query, param, _transaction);
        }

        public int Execute<T>(string query, T t)
        {
            var count = _connection.Execute(query, t, _transaction);
            return count;
        }
    }
}