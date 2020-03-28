using System;

namespace FM.Portal.Core.Model
{
   public class DeleteAttachmentVM
    {
        public Guid ID { get; set; }
        public string FileName { get; set; }
        public PathType PathType { get; set; }
    }
}
