using System;
using System.ComponentModel;

namespace CmdTool
{
    public class Command : Attribute
    {
        public string Command { get; private set; }

        public Command(string command)
        {
            Command = command;
        }
    }

    public class Option : Attribute
    {
        public string Command { get; private set; }
        public string Description { get; private set; }
        public bool Required { get; private set; }

        public Option(string command, string description, bool required)
        {
            Command = command;
            Description = description;
            Required = required;
        }
    }
}