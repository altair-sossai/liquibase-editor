using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using LiquibaseEditor.Commands;
using LiquibaseEditor.Extensions;
using LiquibaseEditor.Factories;
using LiquibaseEditor.Services;
using LiquibaseEditor.Validators;

namespace LiquibaseEditor.ViewModels
{
    public class DiffViewModel
    {
        private DiffCommand _command;

        public DiffViewModel()
        {
            Command = new DiffCommand
            {
                Author = "altair.sossai",
                SourceDatabase = Constants.Databases.SqlServer,
                TargetDatabase = Constants.Databases.SqlServer,
                DirectoryPath = @"E:\liquibase-editor",
                TableNames = "GA_TIMESHEET; GA_TIMESHEET_ITEM"
            };

            Command.UseSqlServerDefaultConfiguration();
        }

        public List<string> Databases { get; set; } = Constants.Databases.Types;

        public DiffCommand Command
        {
            get => _command;
            set
            {
                _command = value;

                if (_command == null)
                    return;

                _command.PropertyChanged -= CommandPropertyChanged;
                _command.PropertyChanged += CommandPropertyChanged;
            }
        }

        private void CommandPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SourceDatabasePropertyChanged(e);
            TargetDatabasePropertyChanged(e);
        }

        private void SourceDatabasePropertyChanged(PropertyChangedEventArgs e)
        {
            if (!e.PropertyName.Equals(nameof(DiffCommand.SourceDatabase)))
                return;

            switch (Command.SourceDatabase)
            {
                case Constants.Databases.SqlServer:
                    Command.UseSourceSqlServerDefaultConfiguration();
                    break;

                case Constants.Databases.Oracle:
                    Command.UseSourceOracleDefaultConfiguration();
                    break;
            }
        }

        private void TargetDatabasePropertyChanged(PropertyChangedEventArgs e)
        {
            if (!e.PropertyName.Equals(nameof(DiffCommand.TargetDatabase)))
                return;

            switch (Command.TargetDatabase)
            {
                case Constants.Databases.SqlServer:
                    Command.UseTargetSqlServerDefaultConfiguration();
                    break;

                case Constants.Databases.Oracle:
                    Command.UseTargetOracleDefaultConfiguration();
                    break;
            }
        }

        public void Diff()
        {
            try
            {
                using (var sourceUnitOfWork = UnitOfWorkFactory.New(Command.SourceDatabase, Command.SourceConnectionString))
                using (var targetUnitOfWork = UnitOfWorkFactory.New(Command.TargetDatabase, Command.TargetConnectionString))
                {
                    var sourceTableRepository = TableRepositoryFactory.New(sourceUnitOfWork);
                    var targetTableRepository = TableRepositoryFactory.New(targetUnitOfWork);
                    var validator = new DiffViewModelValidator();

                    var diffService = new DiffService(sourceTableRepository, targetTableRepository, validator);

                    diffService.Diff(Command);
                }

                MessageBox.Show("Success");
                Process.Start("explorer.exe", Command.DirectoryPath);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show(exception.ToString());
            }
        }
    }
}