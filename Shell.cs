using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;
using ShellProject.Common;

namespace ShellProject
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
                Execute(input);
            } while (input != "exit");
        }
        public int Execute(string input)
        {
            if (Commands.Exists(x => x.alias == input))
            {
                var process = new Process();
                process.StartInfo = new ProcessStartInfo(Commands.Find(x => x.alias == input).exePath)
                {
                    UseShellExecute = false
                };
                process.Start();
                process.WaitForExit();
                return 0;
            }

            Console.WriteLine($"Command: {input} not founded");
            return 1;
        }
    }
}