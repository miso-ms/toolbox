using System;
using System.ComponentModel;
using CmdTool;

namespace CmdTool
{
    [CmdTool("test")]
    [CmdDescription("This is a simple test class with some arguments.")]
    [CmdDescription("usage: TestCommand1 [<options] <directory>")]
    class TestCommand
    {
        [Option("-v", "Verbose flag", true)]
        public const bool Verbose = false;

        public void Run()
        {
            Console.WriteLine(Verbose);
        }
    }

    [CmdTool("test2")]
    [CmdDescription("hello weird c# features")]
    [CmdDescription("usage: TestCommand2 [<options] <directory>")]
    class TestCommand2
    {
        [Option("-v", "Verbose flag", true)]
        public const bool Verbose = false;

        [Option("-r", "res", true)]
        public const bool Retry = false;

        public void Run()
        {
            Console.WriteLine(Verbose);
            Console.WriteLine(Retry);
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
                Console.WriteLine(Load.GetHelp());
                Console.WriteLine(Load.GetHelp("test2"));
                Console.ReadLine();
                return 0;
            }

            Console.ReadLine();
            return 0;
        }
    }
}
