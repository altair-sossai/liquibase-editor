using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using LiquibaseEditor.Commands;
using LiquibaseEditor.Factories;
using LiquibaseEditor.Services;
using LiquibaseEditor.Validators;

namespace LiquibaseEditor.ViewModels
{
    public class GenerateViewModel
    {
        public GenerateViewModel()
        {
            Command = new GenerateCommand();
        }

        public List<string> Databases { get; set; } = new List<string>
        {
            "SQL Server"
        };

        public GenerateCommand Command { get; set; }

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