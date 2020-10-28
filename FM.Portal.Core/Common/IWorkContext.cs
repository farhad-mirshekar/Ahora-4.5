using FM.Portal.Core.Model;

namespace FM.Portal.Core.Common
{
   public interface IWorkContext
    {
        Language WorkingLanguage { get; set; }
    }
}
