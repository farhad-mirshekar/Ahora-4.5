using FM.Portal.BaseModel;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Model
{
   public class Role : Entity
    {
        public Guid ApplicationID { get; set; }
        public string Name { get; set; }
        public List<Command> Permissions { get; set; }
        public string Json { get; set; } //only for command but not exist in table role
    }
}
