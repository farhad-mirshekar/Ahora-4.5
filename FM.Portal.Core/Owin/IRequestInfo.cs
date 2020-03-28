using System;

namespace FM.Portal.Core.Owin
{
   public interface IRequestInfo
    {
        Guid? UserId { get; }
        string UserName { get; }
        Guid? ApplicationId { get; }
        Guid? PositionId { get; }
        Guid? DepartmentId { get; }
    }
}
