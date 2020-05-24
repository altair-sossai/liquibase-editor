using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LiquibaseEditor.Commands
{
    public class GenerateCommand : INotifyPropertyChanged
    {
        private string _author = "altair.sossai";
        private string _connectionString = "Server=localhost;Database=FC_SIMPLEFARM_DEV2004;User Id=sa;Password=ef66b58b-6ff2-4c78-bcec-6b279312b625;";
        private string _database = "SQL Server";
        private string _directoryPath = @"E:\liquibase-editor";
        private string _tableNames = "GA_TIMESHEET; GA_TIMESHEET_ITEM";

        public string Database
        {
            get => _database;
            set
            {
                if (value == _database) return;
                _database = value;
                OnPropertyChanged();
            }
        }

        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                if (value == _connectionString) return;
                _connectionString = value;
                OnPropertyChanged();
            }
        }

        public string TableNames
        {
            get => _tableNames;
            set
            {
                if (value == _tableNames) return;
                _tableNames = value;
                OnPropertyChanged();
            }
        }

        public string DirectoryPath
        {
            get => _directoryPath;
            set
            {
                if (value == _directoryPath) return;
                _directoryPath = value;
                OnPropertyChanged();
            }
        }

        public string Author
        {
            get => _author;
            set
            {
                if (value == _author) return;
                _author = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}