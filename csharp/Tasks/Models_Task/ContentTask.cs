using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Models_Task
{
    public class ContentTask
    {
        public ContentTask(DescriptionTask description,DoneTask done) 
        {
            Description = description;
            Done = done;
        }
        public DescriptionTask Description { get; }
        public DoneTask Done { get; }

    }
}
