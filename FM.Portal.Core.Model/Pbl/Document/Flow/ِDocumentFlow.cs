using FM.Portal.BaseModel;
using FM.Portal.Core.Common;
using System;

namespace FM.Portal.Core.Model
{
    public class DocumentFlow : DocumentFlow<DocState>
    {

    }
    public class DocumentFlow<TDocState> : Entity
    {
        public Guid DocumentID { get; set; }
        public TDocState FromDocState { get; set; }
        public Guid? FromPositionID { get; set; }
        public Guid ToPositionID { get; set; }
        public DateTime Date { get; set; }
        public DocumentType Type { get; set; }
        public string FromUserFullName { get; set; }
        public PositionType FromUserPositionType { get; set; }
        public PositionType ToUserPositionType { get; set; }
        public string FromDepartmentName { get; set; }
        public Guid FromDepartmentID { get; set; }
        public TDocState ToDocState { get; set; }
        public SendDocumentType SendType { get; set; }
        public string Comment { get; set; }
        public DateTime? ReadDate { get; set; }
        public DateTime? ActionDate { get; set; }
        public string ToUserFullName { get; set; }
        public string Username { get; set; }
        public string ToDepartmentName { get; set; }
        public Guid ToDepartmentID { get; set; }
        public Guid FromUserID { get; set; }

        public string ReadDatePersian
        {
            get
            {
                if (ReadDate.HasValue)
                    return Helper.GetPersianDate(ReadDate.Value);
                else
                    return null;
            }
        }
        public string ActionDatePersian
        {
            get
            {
                if (ActionDate.HasValue)
                    return Helper.GetPersianDate(ActionDate.Value);
                else
                    return null;
            }
        }
        public string DatePersian => Helper.GetPersianDate(Date);
    }
}
