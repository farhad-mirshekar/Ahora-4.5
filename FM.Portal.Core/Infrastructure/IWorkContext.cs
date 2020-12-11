using FM.Portal.Core.Model;
using System;

namespace FM.Portal.Core.Infrastructure
{
   public interface IWorkContext
    {
        Language WorkingLanguage { get; set; }
        User User { get; set; }
        bool IsAdmin { get; set; }
        Guid? ShoppingID { get; }
        int ShoppingCartItemCount { get; set; }
    }
}
