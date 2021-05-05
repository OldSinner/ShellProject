using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using shell.Common;
using Newtonsoft.Json;

namespace shell
{
    public class Shell
    {
        List<Command> Commands = new List<Command>();
        string path;
        string[] welcomeMessage;
        public int Configure()
        {
            try
            {

                path = Directory.GetCurrentDirectory();
                welcomeMessage = File.ReadAllLines(Directory.GetCurrentDirectory() + "/Configure/WelcomeMessage.txt");
                var jsonConfigFile = File.ReadAllText(Directory.GetCurrentDirectory() + "/Configure/commandConfig.json");
                Commands = JsonConvert.DeserializeObject<List<Command>>(jsonConfigFile);
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
                Console.Write(path + "> ");
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
            if(String.IsNullOrWhiteSpace(input))
            {
                return 1;
            }
            string arguments = string.Empty;
            string[] inputs = input.Split(' ');
            if (inputs.Length > 1)
            {
                arguments = input.Substring(inputs[0].Length + 1);
            }

            if (Commands.Exists(x => x.alias == inputs[0]))
            {
                var process = new Process();
                process.StartInfo = new ProcessStartInfo(Directory.GetCurrentDirectory()+Commands.Find(x => x.alias == inputs[0]).exePath)
                {
                    UseShellExecute = false,
                    Arguments = arguments,
                    WorkingDirectory = path
                };
                process.Start();
                process.WaitForExit();
                var x = process.StandardOutput.ReadToEnd();
                Console.WriteLine(x);
                return 0;
            }

            Console.WriteLine($"Command:{input} not founded");
            return 1;
        }
    }
}