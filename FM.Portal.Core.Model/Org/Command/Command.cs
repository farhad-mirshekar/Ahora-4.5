
using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class Command : Entity
    {
        public Guid ParentID { get; set; }

        public string Node { get; set; }

        public string ParentNode { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Title { get; set; }

        public CommandsType Type { get; set; }
    }
}
