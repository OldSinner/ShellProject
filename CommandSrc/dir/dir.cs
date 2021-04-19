using System;
using System.IO;

namespace dir
{
    class Program
    {
        static void dir(string[] args)
        {
            int minarg = 0;
            int maxarg = 1;
            string path;
            if (args.Length > maxarg || args.Length < minarg)
            {
                Console.WriteLine($"Wrong Number of Arguments: Arguments Wanted: {minarg} - {maxarg}, get: {args.Length}");
                Environment.Exit(0);
            }
            if (args.Length == 0) path = Directory.GetCurrentDirectory();
            else path = args[0];
            try
            {
                Console.WriteLine("Directory: " + path);
                var files = Directory.GetFiles(path);
                var directories = Directory.GetDirectories(path);
                foreach (var dir in directories)
                {
                    Console.WriteLine("/" + new DirectoryInfo(dir).Name);
                }

                foreach (var file in files)
                {
                    Console.WriteLine(new DirectoryInfo(file).Name);
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message + "at " + path);
            }

        }
    }
}
