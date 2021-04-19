using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ShellProject
{
    public class Shell
    {
        Dictionary<string, string> Commands = new Dictionary<string, string>();
        string[] welcomeMessage;
        public int Configure()
        {
            try
            {
                welcomeMessage = File.ReadAllLines(Directory.GetCurrentDirectory() + "/Configure/WelcomeMessage.txt");
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
                Console.Write("$> ");
                input = Console.ReadLine();
                Execute(input);
            } while (input != "exit");
        }
        public int Execute(string input)
        {
            if (Commands.ContainsKey(input))
            {
                var process = new Process();
                process.StartInfo = new ProcessStartInfo(Commands[input])
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