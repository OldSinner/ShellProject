using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Shell.Common
{
    public class BuiltInCommand
    {
        Dictionary<string, string> builtCommand = new Dictionary<string, string>();
        List<Command> Commands = new List<Command>();
        public BuiltInCommand(List<Command> Commands)
        {
            builtCommand.Add("cd", "Use the cd command to move from your present directory to another directory.\n Syntax: \n" +
            "cd <path> \n\n Example: \n cd windows \n cd C:/Windows \n cd ..");
            builtCommand.Add("mkdir", "Use the cd command to move from your present directory to another directory.\n Syntax: \n" +
            "cd <path> \n\n Example: \n cd windows \n cd C:/Windows \n cd ..");
            this.Commands = Commands;
        }
        public bool isCommand(string command, string args)
        {
            switch (command)
            {
                case "help":
                    help(args.Split(' '));
                    return true;
                case "cd":
                    changeDirectory(args.Split(' '));
                    return true;
                case "mkdir":
                    makeDirectory(args.Split(' '));
                    return true;
                default:
                    return false;
            }
        }
        void help(string[] args)
        {
            int minarg = 0;
            int maxarg = 1;
            if (args.Length > maxarg || args.Length < minarg)
            {
                Console.WriteLine($"Wrong Number of Arguments: Arguments Wanted: {minarg} - {maxarg}, get: {args.Length}");
            }
            if (String.IsNullOrEmpty(args[0]))
            {
                Console.WriteLine("List of built in Commands:");
                foreach (var command in builtCommand)
                {
                    Console.WriteLine(command.Key + "\n---------\n" + command.Value);
                    Console.WriteLine("\n\n");
                }
                Console.WriteLine("List of external Commands:");
                foreach (var command in Commands)
                {
                    Console.WriteLine(command.alias + "\n---------\n" + command.description);
                    Console.WriteLine("");
                }
            }
            else
            {
                if(builtCommand.ContainsKey(args[0]))
                {
                    var bCommand = builtCommand.Where(x=>x.Key == args[0]).FirstOrDefault();
                    Console.WriteLine(bCommand.Key + "\n---------\n" + bCommand.Value);
                    return;
                }
                var eCommand = Commands.Where(x=> x.alias == args[0]).FirstOrDefault();
                if(eCommand != null)
                {
                    Console.WriteLine(eCommand.alias + "\n---------\n" + eCommand.description);
                    return;
                }
                Console.WriteLine($"Didnt find command named {args[0]}");
                
            }


        }
        void changeDirectory(string[] args)
        {
            int minarg = 0;
            int flag = 1;
            int maxarg = 1;
            string tempPath = "";
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
        void makeDirectory(string[] args)
        {
            int minarg = 1;
            int maxarg = 1;
            if (args.Length > maxarg || args.Length < minarg)
            {
                Console.WriteLine($"Wrong Number of Arguments: Arguments Wanted: {minarg} - {maxarg}, get: {args.Length}");
            }
            try
            {
                if (!String.IsNullOrEmpty(args[0]))
                {
                    args[0].Replace('\\', '/');
                    if (Path.IsPathRooted(args[0]))
                    {
                        if (!Directory.Exists(args[0]))
                            Directory.CreateDirectory(args[0]);
                        else
                        {
                            Console.WriteLine($"An item with the specified name {args[0]} already exists.");
                            return;
                        }

                    }
                    else if (args[0][0] == '/')
                    {
                        var drive = Directory.GetDirectoryRoot(Env.path);
                        var path = drive + args[0];
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        else
                        {
                            Console.WriteLine($"An item with the specified name {args[0]} already exists.");
                            return;
                        }
                    }
                    else
                    {
                        var path = Path.Join(Env.path, args[0]);
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        else
                        {
                            Console.WriteLine($"An item with the specified name {args[0]} already exists.");
                            return;
                        }
                    }
                    Console.WriteLine("Directory: " + Directory.GetParent(args[0]));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }



        }

    }
}