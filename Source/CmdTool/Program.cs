using System;
using System.ComponentModel;
using CmdTool;

namespace CmdTool
{
    [Command("test")]
    [Description("This is a simple test class with some arguments.")]
    class TestCommand
    {
        [Option("-v", "Verbose flag", true)]
        private const bool Verbose = false;

        public void Run()
        {
            Console.WriteLine(Verbose);
        }
    }

    /// <summary>
    /// Collection of command line scripts.
    /// </summary>
    class Program
    {

        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No arguments found, use -help for instructions.");
                return 0;
            }

            Console.ReadLine();
            return 0;
        }
    }
}
