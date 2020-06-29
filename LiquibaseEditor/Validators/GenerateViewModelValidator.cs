using System;
using System.IO;
using System.Linq;
using FluentValidation;
using LiquibaseEditor.Commands;
using LiquibaseEditor.Factories;
using LiquibaseEditor.Helpers;

namespace LiquibaseEditor.Validators
{
    public class GenerateViewModelValidator : AbstractValidator<GenerateCommand>
    {
        public GenerateViewModelValidator()
        {
            RuleFor(r => r.Database)
                .NotEmpty();

            RuleFor(r => r.ConnectionString)
                .NotEmpty()
                .Must(ValidConnectionString);

            RuleFor(r => r.TableNames)
                .NotEmpty()
                .Must(ValidTableNames);

            RuleFor(r => r.DirectoryPath)
                .NotEmpty()
                .Must(Directory.Exists);

            RuleFor(r => r.Author)
                .NotEmpty();
        }

        private static bool ValidConnectionString(GenerateCommand command, string connectionString)
        {
            try
            {
                using var unitOfWork = UnitOfWorkFactory.New(command.Database, connectionString);

                return unitOfWork.IsConnected();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return false;
            }
        }

        private static bool ValidTableNames(GenerateCommand command, string tableNames)
        {
            try
            {
                if (command.AllTables)
                    return true;

                var names = TableHelper.ParseTableNames(tableNames);

                using var unitOfWork = UnitOfWorkFactory.New(command.Database, command.ConnectionString);
                var tableRepository = TableRepositoryFactory.New(unitOfWork);
                var tables = tableRepository.GetAll();

                return names.All(name => tables.Any(table => string.Equals(table.Name, name, StringComparison.CurrentCultureIgnoreCase)));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return false;
            }
        }
    }
}