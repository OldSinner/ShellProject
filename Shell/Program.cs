using System;

namespace Shell
{
    class Program
    {
        static void Main(string[] args)
        {
            var shell = new Shell();
            if(shell.Configure() == 0)
            {
                shell.Run();
            }
            
        }
    }
}
