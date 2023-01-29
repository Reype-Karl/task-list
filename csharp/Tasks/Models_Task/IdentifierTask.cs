using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Models_Task
{
    public class IdentifierTask
    {
        public long Value { get; private set; } = 0;

        public long NewIdentifier(long value) => Value = value;
        public long NextIdentifier() => ++Value;
    }
}
