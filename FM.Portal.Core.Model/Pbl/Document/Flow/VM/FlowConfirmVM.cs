using System;

namespace FM.Portal.Core.Model
{
   public class FlowConfirmVM
    {
        public SendDocumentType SendType { get; set; }
        public string Comment { get; set; }
        public Guid DocumentID { get; set; }
    }
}
