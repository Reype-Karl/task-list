using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Models_Task
{
    internal class Task
    {
        public Task(IdentifierTask identifier, ContentTask contentTask)
        {
            Identifier = identifier;
            Content = contentTask;
        }

        // 1,2,4,5,6,8,9
        public IdentifierTask Identifier { get; }
        public ContentTask Content { get;}

    }
}
