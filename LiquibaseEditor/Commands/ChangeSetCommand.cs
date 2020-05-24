using System;

namespace LiquibaseEditor.Commands
{
    public class ChangeSetCommand
    {
        public ChangeSetCommand(string author)
            : this()
        {
            Author = author;
        }

        public ChangeSetCommand()
        {
            Date = DateTime.Now;
        }

        public static int Sequence { get; private set; } = 1;
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public string Id => $"{Date:yyyyMMdd}-{Sequence}";

        public static void Next()
        {
            Sequence++;
        }

        public static void Restart()
        {
            Sequence = 1;
        }
    }
}