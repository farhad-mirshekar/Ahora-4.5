using FM.Portal.Core.Model;
using System.Collections.Generic;

namespace Ahora.WebApp.Models.Pbl
{
    public class TagsListModel
    {
        public List<TagSearchVM> AvailableTags { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string TagNameSearch { get; set; }
    }
}