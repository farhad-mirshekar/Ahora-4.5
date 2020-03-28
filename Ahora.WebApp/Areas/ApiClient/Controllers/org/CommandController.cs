using FM.Portal.FrameWork.Api.Controller;
using FM.Portal.Core.Service;
using System.Web.Http;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using System;
using FM.Portal.Core.Result;
using System.Collections.Generic;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/command")]
    public class CommandController : BaseApiController<ICommandService>
    {
        public CommandController(ICommandService service) : base(service)
        {
        }

        [HttpPost,Route("Get/{ID:guid}")]
        public IHttpActionResult Get(Guid ID)
        {
            try
            {
                var result = _service.Get(ID);
                return Ok(result);
            }
            catch { throw; }
        }
        [HttpPost , Route("List")]
        public IHttpActionResult List()
        {
            try
            {
                var result = _service.List();
                return Ok(result);
            }
            catch { throw; }
        }
        [HttpPost, Route("ListForRole")]
        public IHttpActionResult ListForRole(CommandListVM model)
        {
            try
            {
                var result = _service.ListForRole(model);
                return Ok(result);
            }
            catch { throw; }
        }

        [HttpPost, Route("ListByNode")]
        public IHttpActionResult ListByNode(NodeVM model)
        {
            try
            {
                var result = _service.ListByNode(model.Node);
                return Ok(result);
            }
            catch { throw; }
        }

        [HttpPost, Route("CommandType")]
        public IHttpActionResult CommandType()
        {
            try
            {
                var result = EnumExtensions.GetValues<CommandsType>();
                if (result.Count > 0)
                    return Ok(Result<List<EnumCast>>.Successful(data: result));
                else
                    return Ok(Result<List<EnumCast>>.Failure());
            }
            catch { return NotFound(); }
        }
        [HttpPost,Route("Add")]
        public IHttpActionResult Add(Command model)
        {
            try
            {
                var result = _service.Add(model);
                return Ok(result);
            }
            catch (Exception e) { return NotFound(); }
        }

        [HttpPost, Route("Edit")]
        public IHttpActionResult Edit(Command model)
        {
            try
            {
                var result = _service.Edit(model);
                return Ok(result);
            }
            catch (Exception e) { return NotFound(); }
        }

        [HttpPost, Route("GetPermission")]
        public IHttpActionResult GetPermission()
        {
            try
            {
                var result = _service.GetPermission();
                return Ok(result);
            }
            catch (Exception e) { return NotFound(); }
        }
    }
}
