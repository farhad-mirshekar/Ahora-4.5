using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
   public class Department:Entity
    {
        public Guid ParentID { get; set; }
        public string Node { get; set; }
        public string ParentNode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string CodePhone { get; set; }
        public bool Enabled { get; set; }
        public Guid RemoverID { get; set; }
        public DateTime RemoverDate { get; set; }
    }
}
