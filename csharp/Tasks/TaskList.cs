using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Tasks.Models_Task;

namespace Tasks
{
	public sealed class TaskList
	{
		private const string Quit = "quit";

		private readonly Projects _projects = new();
		private readonly IConsole _console;

        private readonly IdentifierTask _lastIdentifier = new ();
        private char[] separator_space = " ".ToCharArray();

        public static void Main()
		{
			new TaskList(new RealConsole()).Run(CancellationToken.None);
		}

		public TaskList(IConsole console)
		{
			this._console = console;
		}

		public void Run(CancellationToken token)
		{
			while (RunOnce())
			{
				token.ThrowIfCancellationRequested();
			}
		}

		private bool RunOnce()
		{
            _console.Write("> ");
            var command = _console.ReadLine();
			if (command == Quit) return false;

            Execute(command);
            return true;
		}
		/// Raccourcir à partir de ici
		
		private void Execute(string commandLine)
		{
            var commandRest = commandLine.Split(separator_space, 2);
			var command = commandRest[0];
			switch (command) {
				case "show":
					Show();
                    return;
				case "add":
					Add(commandRest[1]);
                    return;
				case "check":
					Check(commandRest[1]);
                    return;
				case "uncheck":
					Uncheck(commandRest[1]);
                    return;
				case "help":
					Help();
                    return;
			}
			Error(command);
		}

		private void Show() => _projects.PrintInto(_console);
		private void Add(string commandLine)
		{
            var subcommandRest = commandLine.Split(separator_space, 2);
			var subcommand = subcommandRest[0];

			if (subcommand == "project") {
				AddProject(subcommandRest[1]);
				return;
			}

			if (subcommand == "task") {
				var projectTask = subcommandRest[1].Split(separator_space, 2);
				AddTask(projectTask[0], projectTask[1]);
			}
		}

		private void AddProject(string name) => _projects.Add(name);

		private void AddTask(string project, string description)
		{
            _lastIdentifier.NextIdentifier();

            var descriptionTask = new DescriptionTask();
            var doneTask = new DoneTask();
            var identifierTask = new IdentifierTask();
			var _lastIdentifierValue = _lastIdentifier.Value;

            identifierTask.NewIdentifier(_lastIdentifierValue);
            descriptionTask.AddDescription(description);
			doneTask.IsDone(false);

            _projects.AddTaskToProject(project, new Task (identifierTask, new ContentTask(descriptionTask, doneTask)), _console);
        }

		private void Check(string idString)
		{
			SetDone(idString, true);
		}

		private void Uncheck(string idString)
		{
			SetDone(idString, false);
		}

		private void SetDone(string idString, bool done) => _projects.SetTaskDone(idString, done, _console);

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
