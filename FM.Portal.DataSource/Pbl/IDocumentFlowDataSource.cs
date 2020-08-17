﻿using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IDocumentFlowDataSource:IDataSource
    {
        Result Insert(DocumentFlow flow);
        DataTable List(DocumentFlowListVM listVM);
        Result SetAsRead(Guid DocumentID);
    }
}
