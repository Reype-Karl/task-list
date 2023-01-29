using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tasks.Models_Task;

namespace Tasks
{
    internal class Project
    {
        private readonly IList<Task> _tasks = new List<Task>();

        public void Add(Task task)
        {
            _tasks.Add(task);
        }

        public void PrintInto(IConsole console)
        {
            foreach (var task in _tasks)
            {
                var identifierTask = task.Identifier;
                var contentTask = task.Content;
                var descriptionTask = contentTask.Description;
                var doneTask = contentTask.Done;

                console.WriteLine($"    [{(doneTask.Value ? 'x' : ' ')}] {identifierTask.Value}: {descriptionTask.Value}");
            }
        }

        public void SetDoneIfExists(string identifier, bool done, IConsole console)
        {
            var long_parse = long.Parse(identifier);

            foreach (var task in _tasks)
            {
                var identifiedTask = task.Identifier;
                if (identifiedTask.Value == long_parse)
                {
                    var identifiedTaskContent = task.Content;
                    var identifiedTaskIsDone = identifiedTaskContent.Done;

                    identifiedTaskIsDone.IsDone(done);
                    return;
                }

            }
        }
    }
}