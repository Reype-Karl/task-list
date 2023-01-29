using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Models_Task
{
    public class DoneTask
    {
        public bool Value { get; private set; } = false;

        public bool IsDone(bool done) => Value = done;
    }
}
