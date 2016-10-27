using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace CmdTool
{
    class Load
    {
        /// <summary>
        /// CmdTool metadata
        /// </summary>
        public struct CmdDesc
        {
            public Type CmdType;
            public Option[] Options;

            public CmdDesc(Type type, Option[] options)
            {
                CmdType = type;
                Options = options;
            }
        }

        // Available Command Line Tools in assembly.
        private static readonly Dictionary<string, CmdDesc> CommandDictionary = GetCmdTools();

        public static string GetHelp()
        {
            var ret = "usage: miso <command> [<args>]\n\n" + 
                         "Available commands:\n";
            foreach (KeyValuePair<string, CmdDesc> elem in CommandDictionary)
            {
                IEnumerable<CmdDescription> description = 
                    elem.Value.CmdType.GetCustomAttributes<CmdDescription>(false);
                ret += $"    {elem.Key}\t{description.First().Description}\n";
            }
            ret += "\nUse 'miso <command> -help' for additional information.\n";
            return ret;
        }

        public static string GetHelp(string type)
        {
            CmdDesc command = CommandDictionary[type];
            IEnumerable<CmdDescription> usage = 
                command.CmdType.GetCustomAttributes<CmdDescription>(false);
            var ret = usage.Aggregate("", (current, elem) => current + $"{elem.Description}\n");
            return command.Options.Aggregate(
                ret + "\n", 
                (current, elem) => current + $"    {elem.Command}\t\t{elem.Description}\n");
        }

        private static Dictionary<string, CmdDesc> GetCmdTools()
        {
            return GetTypesWith<CmdTool>(true)
                .ToDictionary(
                    type => ((CmdTool) Attribute.GetCustomAttribute(type, typeof(CmdTool))).Command,
                    type =>
                    {
                        IEnumerable<Option> options =
                            from fields in type.GetFields()
                            from opt in fields.GetCustomAttributes<Option>()
                            select opt;
                        return new CmdDesc(type, options.ToArray());
                    },
                    StringComparer.OrdinalIgnoreCase);
        }

        private static IEnumerable<Type> GetTypesWith<TAttribute>(bool inherit)
        {
            return
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from types in assembly.GetTypes()
                where types.IsDefined(typeof(TAttribute), inherit)
                select types;
        }
    }
}
