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
using LiquibaseEditor.Validators;

namespace LiquibaseEditor.Services
{
    public class DiffService
    {
        private readonly ITableRepository _sourceTableRepository;
        private readonly ITableRepository _targetTableRepository;
        private readonly DiffViewModelValidator _validator;

        public DiffService(ITableRepository sourceTableRepository,
            ITableRepository targetTableRepository,
            DiffViewModelValidator validator)
        {
            _sourceTableRepository = sourceTableRepository;
            _targetTableRepository = targetTableRepository;
            _validator = validator;
        }

        public void Diff(DiffCommand command)
        {
            _validator.ValidateAndThrow(command);

            ChangeSetCommand.Restart();

            DropColumns(command);
            AddColumns(command);
        }

        private void DropColumns(DiffCommand command)
        {
            var tables = _sourceTableRepository.GetAll();
            var tableNames = command.AllTables ? tables.Select(t => t.Name).ToList() : TableHelper.ParseTableNames(command.TableNames);

            foreach (var table in tables.Where(t => tableNames.Contains(t.Name)))
                DropColumns(command, table);
        }

        private void DropColumns(DiffCommand command, Table table)
        {
            var sourceColumns = _sourceTableRepository.GetColumns(table.Name);
            var targetColumns = _targetTableRepository.GetColumns(table.Name);
            var dropColumns = targetColumns.Where(t => sourceColumns.All(s => s.Name != t.Name)).ToList();

            foreach (var dropColumn in dropColumns)
                DropColumn(command, table, dropColumn);
        }

        private static void DropColumn(DiffCommand command, Table table, Column dropColumn)
        {
            var changeSetCommand = new ChangeSetCommand(command.Author);
            var builder = new DropColumnChangeSetBuilder(table, dropColumn);
            var changeSet = builder.Build(changeSetCommand);

            var xml = changeSet.ToXml();
            var path = Path.Combine(command.DirectoryPath, $"{ChangeSetCommand.Sequence:000}.{table.Name.ToLower()}.{dropColumn.Name.ToLower()}.drop.xml");
            WriteFile(path, xml);

            ChangeSetCommand.Next();
        }

        private void AddColumns(DiffCommand command)
        {
            var tables = _sourceTableRepository.GetAll();
            var tableNames = command.AllTables ? tables.Select(t => t.Name).ToList() : TableHelper.ParseTableNames(command.TableNames);

            foreach (var table in tables.Where(t => tableNames.Contains(t.Name)))
                AddColumns(command, table);
        }

        private void AddColumns(DiffCommand command, Table table)
        {
            var sourceColumns = _sourceTableRepository.GetColumns(table.Name);
            var targetColumns = _targetTableRepository.GetColumns(table.Name);
            var dropColumns = sourceColumns.Where(t => targetColumns.All(s => s.Name != t.Name)).ToList();

            foreach (var dropColumn in dropColumns)
                AddColumn(command, table, dropColumn);
        }

        private static void AddColumn(DiffCommand command, Table table, Column dropColumn)
        {
            var changeSetCommand = new ChangeSetCommand(command.Author);
            var builder = new AddColumnChangeSetBuilder(table, dropColumn);
            var changeSet = builder.Build(changeSetCommand);

            var xml = changeSet.ToXml();
            var path = Path.Combine(command.DirectoryPath, $"{ChangeSetCommand.Sequence:000}.{table.Name.ToLower()}.{dropColumn.Name.ToLower()}.add.xml");
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