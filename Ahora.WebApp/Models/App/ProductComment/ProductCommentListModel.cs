using System.Collections.Generic;
using @Model = FM.Portal.Core.Model;
using FM.Portal.Core.Model;

namespace Ahora.WebApp.Models.App
{
    public class ProductCommentListModel
    {
        public List<Model.ProductComment> AvailableComments { get; set; }
        public User User { get; set; }
        public Model.Product Product { get; set; }
    }
}