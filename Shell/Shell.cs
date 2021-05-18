using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Shell.Common;
using Newtonsoft.Json;

namespace Shell
{
    public class Shell
    {
        List<Command> Commands = new List<Command>();
        BuiltInCommand basicComannd;
        string[] welcomeMessage;
        public int Configure()
        {
            Console.Clear();
            try
            {
                Console.WriteLine("Shell Starting...");
                Env.path = Directory.GetCurrentDirectory();
                Console.WriteLine("Load external commands...");
                var jsonConfigFile = File.ReadAllText(Env.path + "/Configure/commandConfig.json");
                Commands = JsonConvert.DeserializeObject<List<Command>>(jsonConfigFile);
                welcomeMessage = File.ReadAllLines(Env.path + "/Configure/WelcomeMessage.txt");
                Console.WriteLine("Load Built in commands...");
                basicComannd = new BuiltInCommand(Commands);
                Console.Clear();
                return 0;
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                Console.WriteLine("Error occured during Configure! Shell Cannot be used!");
                return 1;
            }

        }
        public void Run()
        {
            string input = null;
            foreach (var line in welcomeMessage)
            {
                Console.WriteLine(line);
            }
            do
            {
                Console.Write(Env.path + "> ");
                input = Console.ReadLine();
                try
                {
                    Execute(input);
                }
                catch (Exception x)
                {
                    Console.WriteLine(x.Message);
                }

            } while (input != "exit");
        }
        public int Execute(string input)
        {
            // Input conversion
            if (String.IsNullOrWhiteSpace(input))
            {
                return 1;
            }
            string arguments = string.Empty;
            string[] inputs = input.Split(' ');
            if (inputs.Length > 1)
            {
                arguments = input.Substring(inputs[0].Length + 1);
            }
            inputs[0] = inputs[0].ToLower();
            // Execute
            if (!basicComannd.isCommand(inputs[0], arguments))
            {
                if (Commands.Exists(x => x.alias == inputs[0]))
                {
                    var process = new Process();
                    string processPath = Commands.Find(x => x.alias == inputs[0]).exePath;
                    if (processPath[0] == '.') processPath = Directory.GetCurrentDirectory() + processPath.Substring(1);
                    process.StartInfo = new ProcessStartInfo(processPath)
                    {
                        UseShellExecute = false,
                        Arguments = arguments,
                        WorkingDirectory = Env.path
                    };
                    process.Start();
                    process.WaitForExit();
                    return 0;
                }
                else
                {
                    Console.WriteLine($"Command: {input} not founded");
                }
            }
            return 1;
        }
    }
}