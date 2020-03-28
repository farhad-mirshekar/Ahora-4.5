using FM.Portal.Core.Model.Ptl;
using FM.Portal.Core.Service.Ptl;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Linq;
using System.Web.Http;
using FM.Portal.Core.Extention.CategoryPortal;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/categoryPortal")]
    public class CategoryPortalController : BaseApiController<ICategoryService>
    {
        public CategoryPortalController(ICategoryService service) : base(service)
        {
        }

        [HttpPost, Route("Add")]
        public IHttpActionResult Add(Category model)
        {
            try
            {
                var result = _service.Add(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost, Route("Edit")]
        public IHttpActionResult Edit(Category model)
        {
            try
            {
                var result = _service.Edit(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost, Route("List")]
        public IHttpActionResult List()
        {
            try
            {
                var result = _service.List();
                var model = result.Data.Select(x =>
                {
                    var category = new Category();
                    category.ID = x.ID;
                    category.ParentID = x.ParentID;
                    category.IncludeInLeftMenu = x.IncludeInLeftMenu;
                    category.IncludeInTopMenu = x.IncludeInTopMenu;
                    category.Title = x.GetFormattedBreadCrumb(_service);
                    return category;

                });
                var results = FM.Portal.Core.Result.Result<System.Collections.Generic.List<Category>>.Successful(data: model.ToList());
                return Ok(results);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost, Route("Get/{ID:guid}")]
        public IHttpActionResult Get(Guid ID)
        {
            try
            {
                var result = _service.Get(ID);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}