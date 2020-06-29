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
    public class GenerateViewModel
    {
        private GenerateCommand _command;

        public GenerateViewModel()
        {
            Command = new GenerateCommand
            {
                Author = "altair.sossai",
                Database = Constants.Databases.SqlServer,
                DirectoryPath = @"E:\liquibase-editor",
                TableNames = "GA_TIMESHEET; GA_TIMESHEET_ITEM"
            };

            Command.UseSqlServerDefaultConfiguration();
        }

        public List<string> Databases { get; set; } = Constants.Databases.Types;

        public GenerateCommand Command
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
            if (!e.PropertyName.Equals(nameof(GenerateCommand.Database)))
                return;

            switch (Command.Database)
            {
                case Constants.Databases.SqlServer:
                    Command.UseSqlServerDefaultConfiguration();
                    break;

                case Constants.Databases.Oracle:
                    Command.UseOracleDefaultConfiguration();
                    break;
            }
        }

        public void Generate()
        {
            try
            {
                using (var unitOfWork = UnitOfWorkFactory.New(Command.Database, Command.ConnectionString))
                {
                    var tableRepository = TableRepositoryFactory.New(unitOfWork);
                    var validator = new GenerateViewModelValidator();
                    var generateService = new GenerateService(tableRepository, validator);

                    generateService.Generate(Command);
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