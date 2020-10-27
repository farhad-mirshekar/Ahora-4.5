using FM.Portal.Core.Extention.Category;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Linq;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/category")]
    public class CategoryController : BaseApiController<ICategoryService>
    {
        public CategoryController(ICategoryService service) : base(service)
        {
        }

        [HttpPost,Route("Add")]
        public IHttpActionResult Add(Category model)
        {
            try
            {
                var result = _service.Add(model);
                return Ok(result);
            }
            catch(Exception e)
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

        [HttpPost, Route("List/{State}")]
        public IHttpActionResult List(string State)
        {
            try
            {
                var result = _service.List();
                switch (State)
                {
                    case "cartable":
                        return Ok(result);
                    case "dropdown":
                        var model = result.Data.Select(x =>
                        {
                            var category = new Category();
                            category.ID = x.ID;
                            category.ParentID = x.ParentID;
                            category.IncludeInLeftMenu = x.IncludeInLeftMenu;
                            category.IncludeInTopMenu = x.IncludeInTopMenu;
                            category.DiscountID = x.DiscountID;
                            category.HasDiscountsApplied = x.HasDiscountsApplied;
                            category.Node = x.Node;
                            category.ParentID = x.ParentID;
                            category.ParentNode = x.ParentNode;
                            category.Title = CategoryExtention.GetFormattedBreadCrumb(x, _service);
                            category.CreationDate = x.CreationDate;
                            return category;

                        });
                        var results = FM.Portal.Core.Result<System.Collections.Generic.List<Category>>.Successful(data: model.ToList());
                        return Ok(results);
                }
                return Ok(result);
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

        [HttpPost, Route("Delete/{ID:guid}")]
        public IHttpActionResult Delete(Guid ID)
        {
            try
            {
                var result = _service.Delete(ID);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}
