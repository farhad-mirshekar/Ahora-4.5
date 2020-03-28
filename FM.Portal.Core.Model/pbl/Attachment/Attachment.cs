using System;

namespace FM.Portal.Core.Model
{
    public class Attachment : FileModel
    {
        public Guid ParentID { get; set; }
        public AttachmentType Type { get; set; }
        public PathType PathType { get; set; }

        public string Path => PathType.ToString();
    }
}


