using System;

namespace LiquibaseEditor.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public string Database { get; }
        bool IsConnected();
    }
}