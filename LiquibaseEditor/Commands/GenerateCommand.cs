using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LiquibaseEditor.Commands
{
    public class GenerateCommand : INotifyPropertyChanged
    {
        private string _author;
        private string _connectionString;
        private string _database;
        private string _directoryPath;
        private string _tableNames;

        public string Database
        {
            get => _database;
            set
            {
                if (value == _database)
                    return;

                _database = value;
                OnPropertyChanged();
            }
        }

        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                if (value == _connectionString)
                    return;

                _connectionString = value;
                OnPropertyChanged();
            }
        }

        public string TableNames
        {
            get => _tableNames;
            set
            {
                if (value == _tableNames)
                    return;

                _tableNames = value;
                OnPropertyChanged();
            }
        }

        public bool AllTables => TableNames?.Equals("*") ?? false;

        public string DirectoryPath
        {
            get => _directoryPath;
            set
            {
                if (value == _directoryPath)
                    return;

                _directoryPath = value;
                OnPropertyChanged();
            }
        }

        public string Author
        {
            get => _author;
            set
            {
                if (value == _author)
                    return;

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