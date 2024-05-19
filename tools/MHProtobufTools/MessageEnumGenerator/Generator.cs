using System.Reflection;
using System.Text;

namespace MessageEnumGenerator
{
    internal class Generator
    {
        private string _root;
        private string _protoDirectory;

        public Generator(string directoryName)
        {
            _root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _protoDirectory = Path.Combine(_root, directoryName);
        }

        public void Generate()
        {
            StringBuilder sb = new();

            foreach (string filePath in Directory.GetFiles(_protoDirectory))
            {
                if (Path.GetExtension(filePath) != ".proto") continue;
                ProcessProtobuf(filePath, sb);
            }

            string outputPath = Path.Combine(_root, "ProtocolEnums.cs");
            File.WriteAllText(outputPath, sb.ToString());
            Console.WriteLine($"Saved output to {outputPath}");
        }

        private void ProcessProtobuf(string filePath, StringBuilder sb)
        {
            Console.WriteLine($"Processing {Path.GetFileNameWithoutExtension(filePath)}...");

            string[] proto = File.ReadAllLines(filePath);

            sb.AppendLine($@"public enum {Path.GetFileNameWithoutExtension(filePath)}Message : uint");
            sb.AppendLine(@"{");

            uint messageId = 0;

            foreach (string line in proto)
            {
                if (line.StartsWith("message") == false) continue;
                sb.AppendLine($"\t{line[8..^2]},");
                messageId++;
            }

            sb.AppendLine(@"}");
            sb.AppendLine();

            Console.WriteLine($"Processed {messageId} messages");
        }
    }
}
