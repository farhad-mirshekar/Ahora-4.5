

using FM.Portal.BaseModel;

namespace FM.Portal.Core.Model
{
   public abstract class FileModel : Entity
    {
        public override string ToString() => FileName;

        public string FileName { get; set; }

        public string Comment { get; set; }

        public byte[] Data { get; set; }

        public string DataString { get; set; }
    }
}
