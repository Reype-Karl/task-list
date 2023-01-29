using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Models_Task
{
    public class DescriptionTask
    {
        public string Value { get; private set; }

        public string AddDescription(string description) => Value = description;
    }
}
