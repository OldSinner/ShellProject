using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ShellProject
{
    public class Shell
    {
        Dictionary<string, string> Commands = new Dictionary<string, string>();
        string welcomeMessage;
        public int configure()
        {
            return 1;
        }
        public void Run()
        {
            string input = null;

            do
            {
                Console.Write("$ ");
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

            Console.WriteLine($"{input} not found");
            return 1;
        }
    }
}