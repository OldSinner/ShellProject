using System;
using System.IO;

namespace Shell.Common
{
    public class BuiltInCommand
    {

        public bool isCommand(string command, string args)
        {
            switch (command)
            {
                case "cd":
                    changeDirectory(args.Split(' '));
                    return true;
                case "help":
                    return true;
                default:
                    return false;
            }
        }
        public void changeDirectory(string[] args)
        {
            int minarg = 0;
            int flag = 1;
            int maxarg = 1;
            string tempPath = "";
            if(String.IsNullOrEmpty(args[0])) Array.Clear(args,0,args.Length);
            if (args.Length > maxarg || args.Length < minarg)
            {
                Console.WriteLine($"Wrong Number of Arguments: Arguments Wanted: {minarg} - {maxarg}, get: {args.Length}");
            }
            if (!String.IsNullOrEmpty(args[0]))    
            {
                try
                {
                    args[0].Replace('\\', '/');
                    if (Path.IsPathRooted(args[0]))
                    {
                        tempPath = args[0];
                        flag = 0;
                    }
                    else if (args[0][0] == '/')
                    {
                        var drive = Directory.GetDirectoryRoot(Env.path);
                        tempPath = drive + args[0];
                    }
                    else
                    {
                        tempPath = Path.Join(Env.path, args[0]);
                    }
                    if (Directory.Exists(tempPath))
                    {
                        Env.path = Path.GetFullPath(tempPath);
                    }
                    else
                    {
                        if (flag == 0)
                            Console.WriteLine("Cannot find drive.");
                        else
                        {
                            Console.WriteLine("Cannot find path " + tempPath);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}