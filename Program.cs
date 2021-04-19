using System;

namespace ShellProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var shell = new Shell();
            shell.configure();
            shell.Run();
        }

    }
}
