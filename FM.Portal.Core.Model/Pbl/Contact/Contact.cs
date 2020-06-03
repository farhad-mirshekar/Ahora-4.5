using FM.Portal.BaseModel;
using FM.Portal.Core.Common;

namespace FM.Portal.Core.Model
{
   public class Contact:Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }

        //only show
        public string CreationDatePersian => Helper.GetPersianDate(CreationDate);
    }
}
