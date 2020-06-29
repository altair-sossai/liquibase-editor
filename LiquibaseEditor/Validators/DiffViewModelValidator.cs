using System;
using System.IO;
using System.Linq;
using FluentValidation;
using LiquibaseEditor.Commands;
using LiquibaseEditor.Factories;
using LiquibaseEditor.Helpers;

namespace LiquibaseEditor.Validators
{
    public class DiffViewModelValidator : AbstractValidator<DiffCommand>
    {
        public DiffViewModelValidator()
        {
            RuleFor(r => r.SourceDatabase)
                .NotEmpty();

            RuleFor(r => r.TargetDatabase)
                .NotEmpty();

            RuleFor(r => r.SourceConnectionString)
                .NotEmpty()
                .Must(ValidSourceConnectionString);

            RuleFor(r => r.TargetConnectionString)
                .NotEmpty()
                .Must(ValidTargetConnectionString);

            RuleFor(r => r.TableNames)
                .NotEmpty()
                .Must(ValidTableNames);

            RuleFor(r => r.DirectoryPath)
                .NotEmpty()
                .Must(Directory.Exists);

            RuleFor(r => r.Author)
                .NotEmpty();
        }

        private static bool ValidSourceConnectionString(DiffCommand command, string connectionString)
        {
            try
            {
                using var unitOfWork = UnitOfWorkFactory.New(command.SourceDatabase, connectionString);

                return unitOfWork.IsConnected();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return false;
            }
        }

        private static bool ValidTargetConnectionString(DiffCommand command, string connectionString)
        {
            try
            {
                using var unitOfWork = UnitOfWorkFactory.New(command.TargetDatabase, connectionString);

                return unitOfWork.IsConnected();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return false;
            }
        }

        private static bool ValidTableNames(DiffCommand command, string tableNames)
        {
            return ValidSourceTableNames(command, tableNames)
                   && ValidTargetTableNames(command, tableNames);
        }

        private static bool ValidSourceTableNames(DiffCommand command, string tableNames)
        {
            try
            {
                if (command.AllTables)
                    return true;

                var names = TableHelper.ParseTableNames(tableNames);

                using var unitOfWork = UnitOfWorkFactory.New(command.SourceDatabase, command.SourceConnectionString);
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

        private static bool ValidTargetTableNames(DiffCommand command, string tableNames)
        {
            try
            {
                if (command.AllTables)
                    return true;

                var names = TableHelper.ParseTableNames(tableNames);

                using var unitOfWork = UnitOfWorkFactory.New(command.TargetDatabase, command.TargetConnectionString);
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