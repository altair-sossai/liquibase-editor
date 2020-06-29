using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LiquibaseEditor.Commands
{
    public class DiffCommand : INotifyPropertyChanged
    {
        private string _author;
        private string _directoryPath;
        private string _sourceConnectionString;
        private string _sourceDatabase;
        private string _tableNames;
        private string _targetConnectionString;
        private string _targetDatabase;

        public string SourceDatabase
        {
            get => _sourceDatabase;
            set
            {
                if (value == _sourceDatabase)
                    return;

                _sourceDatabase = value;
                OnPropertyChanged();
            }
        }

        public string TargetDatabase
        {
            get => _targetDatabase;
            set
            {
                if (value == _targetDatabase)
                    return;

                _targetDatabase = value;
                OnPropertyChanged();
            }
        }

        public string SourceConnectionString
        {
            get => _sourceConnectionString;
            set
            {
                if (value == _sourceConnectionString)
                    return;

                _sourceConnectionString = value;
                OnPropertyChanged();
            }
        }

        public string TargetConnectionString
        {
            get => _targetConnectionString;
            set
            {
                if (value == _targetConnectionString)
                    return;

                _targetConnectionString = value;
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