namespace MessageEnumGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string protobufDirectoryName;
            
            if (args.Length == 0)
            {
                Console.WriteLine("Enter protobuf directory name:");
                protobufDirectoryName = Console.ReadLine();
            }
            else
            {
                protobufDirectoryName = args[0];
            }

            try
            {
                Generator generator = new(protobufDirectoryName);
                generator.Generate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.ReadKey();
        }
    }
}
