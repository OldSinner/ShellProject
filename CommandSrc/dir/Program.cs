using System;
using System.IO;

namespace dir
{
    class Program
    {
        static void dir(string[] args)
        {
            int arg = 1;
            string path;
            if(args.Length >1)
            {
                Console.WriteLine($"Wrong Number of Arguments: Arguments Wanted: {arg}, get: {args.Length}");
                Environment.Exit(0);
            }
            
            if(args.Length==0)
            {
                path = "c:/";
            }
            else
            {
                path = args[0];
            }
            Console.WriteLine("Directory: " + path);
           var files = Directory.GetFiles(path);
            var directories = Directory.GetDirectories(path); 
            Directory.GetCurrentDirectory();
            foreach(var dir in directories)
            {
                Console.WriteLine("/" +new DirectoryInfo(dir).Name);
            }
            
            foreach (var file in files)
            {
                Console.WriteLine(new DirectoryInfo(file).Name);
            }
        }
    }
}
