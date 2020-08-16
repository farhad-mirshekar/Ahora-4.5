using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
    public class BaseDocument<T>:Entity where T : struct, IConvertible 
    {
        public Guid RemoverID { get; set; }
        public DateTime RemoverDate { get; set; }

        public SendType LastSendType { get; set; }

        public T LastFromDocState { get; set; }
        public T LastDocState { get; set; }

        public DateTime LastFlowDate { get; set; }

        public DateTime? LastReadDate { get; set; }

        public Guid LastFromUserID { get; set; }

        public string LastFromUserName { get; set; }

        public Guid LastToPositionID { get; set; }

        public Guid LastFromPositionID { get; set; }

    }
}
