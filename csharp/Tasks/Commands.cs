using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
    public class Commands
    {
        private readonly IConsole _console;
        private char[] separator_space = " ".ToCharArray();
        private void Execute(string commandLine)
        {
            var commandRest = commandLine.Split(separator_space, 2);
            var command = commandRest[0];
            switch (command)
            {
                case "help":
                    Help();
                    return;
            }
            Error(command);
        }

        private void Help()
        {
            _console.WriteLine("Commands:");
            _console.WriteLine("  show");
            _console.WriteLine("  add project <project name>");
            _console.WriteLine("  add task <project name> <task description>");
            _console.WriteLine("  check <task ID>");
            _console.WriteLine("  uncheck <task ID>");
            _console.WriteLine();
        }

        private void Error(string command)
        {
            _console.WriteLine($"I don't know what the command \"{command}\" is.");
        }
    }
}
