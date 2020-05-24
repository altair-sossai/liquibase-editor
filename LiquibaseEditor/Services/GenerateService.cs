using System.IO;
using System.Linq;
using System.Text;
using FluentValidation;
using LiquibaseEditor.Builders;
using LiquibaseEditor.Commands;
using LiquibaseEditor.Entities;
using LiquibaseEditor.Extensions;
using LiquibaseEditor.Helpers;
using LiquibaseEditor.Repositories;

namespace LiquibaseEditor.Services
{
    public class GenerateService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IValidator<GenerateCommand> _validator;

        public GenerateService(ITableRepository tableRepository,
            IValidator<GenerateCommand> validator)
        {
            _tableRepository = tableRepository;
            _validator = validator;
        }

        public void Generate(GenerateCommand command)
        {
            _validator.ValidateAndThrow(command);

            ChangeSetCommand.Restart();

            CreateTable(command);
        }

        private void CreateTable(GenerateCommand command)
        {
            var tables = _tableRepository.GetAll();
            var tableNames = TableHelper.ParseTableNames(command.TableNames);

            foreach (var tableName in tableNames)
            {
                var table = tables.First(f => f.Name == tableName);
                var columns = _tableRepository.GetColumns(tableName);

                var builder = new CreateTableChangeSetBuilder(table, columns);
                var changeSetCommand = new ChangeSetCommand(command.Author);
                var changeSet = builder.Build(changeSetCommand);

                var xml = changeSet.ToXml();
                var path = Path.Combine(command.DirectoryPath, $"{ChangeSetCommand.Sequence:000}.{tableName.ToLower()}.create.xml");
                WriteFile(path, xml);

                ChangeSetCommand.Next();

                foreach (var column in columns.Where(w => w.AutoIncrement))
                    CreateSequence(command, changeSetCommand, table, column);

                var foreignKeys = _tableRepository.GetForeignKeys(tableName);

                foreach (var foreignKey in foreignKeys)
                    AddForeignKeyConstraint(command, changeSetCommand, table, foreignKey);
            }
        }

        private static void CreateSequence(GenerateCommand command, ChangeSetCommand changeSetCommand, Table table, Column column)
        {
            var builder = new CreateSequenceChangeSetBuilder(table);
            var changeSet = builder.Build(changeSetCommand);

            var xml = changeSet.ToXml();
            var path = Path.Combine(command.DirectoryPath, $"{ChangeSetCommand.Sequence:000}.{table.Name.ToLower()}.{column.Name.ToLower()}.sequence.xml");
            WriteFile(path, xml);

            ChangeSetCommand.Next();
        }

        private static void AddForeignKeyConstraint(GenerateCommand command, ChangeSetCommand changeSetCommand, Table table, ForeignKey foreignKey)
        {
            var builder = new AddForeignKeyConstraintChangeSetBuilder(foreignKey);
            var changeSet = builder.Build(changeSetCommand);

            var xml = changeSet.ToXml();
            var path = Path.Combine(command.DirectoryPath, $"{ChangeSetCommand.Sequence:000}.{table.Name.ToLower()}.{foreignKey.Name.ToLower()}.foreign-key.xml");
            WriteFile(path, xml);

            ChangeSetCommand.Next();
        }

        private static void WriteFile(string path, string xml)
        {
            if (File.Exists(path))
                File.Delete(path);

            File.WriteAllText(path, xml, Encoding.Default);
        }
    }
}