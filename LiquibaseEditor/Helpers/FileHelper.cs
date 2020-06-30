using System.IO;
using System.Text;

namespace LiquibaseEditor.Helpers
{
    public static class FileHelper
    {
        public static void Write(string path, string xml)
        {
            if (File.Exists(path))
                File.Delete(path);

            File.WriteAllText(path, xml, Encoding.UTF8);
        }
    }
}