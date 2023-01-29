using System;
using System.Collections.Generic;
using System.Text;
using Tasks.Models_Task;

namespace Tasks
{
    internal class Projects
    {
        private readonly IDictionary<string, Project> _projects= new Dictionary<string, Project>();

        public void Add(string name) 
        {
            _projects.Add(name, new Project());
        }

        public void PrintInto(IConsole console) 
        {
            foreach (var project in _projects)
            {
                var project_key = project.Key;
                var project_value = project.Value;

                console.WriteLine(project_key);
                project_value.PrintInto(console);
                console.WriteLine();
            }
        }

        public void AddTaskToProject(string projectName, Task Task, IConsole console) 
        {
            if (!_projects.TryGetValue(projectName, out Project project))
            {
                console.WriteLine($"Could not find a project with the name \"{projectName}\".");
                return;
            }

            project.Add(Task);
        }

        public void SetTaskDone(string taskIdentifier, bool done, IConsole console)
        {
            foreach (var project in _projects.Values)
            {
                project.SetDoneIfExists(taskIdentifier, done, console);
            }
        }
    }
}