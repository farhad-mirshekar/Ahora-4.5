using System;
using FM.Portal.Core.Tools;
using System.Web.Mvc;
using System.IO;
using System.Web;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using FM.Portal.Core.Model;
using System.Collections.Generic;
using FM.Portal.FrameWork.MVC.Helpers.Files;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    public partial class AttachmentController : BaseController<IAttachmentService>
    {
        public AttachmentController(IAttachmentService service) : base(service) { }

        [HttpPost]
        public virtual ActionResult Upload(PathType type)
        {
            string fileName = string.Empty;
            string filePath = string.Empty;

            try
            {
                if (Request.Files.Count > 0)
                {
                    if (!Request.Files[0].IsValidFile(Request))
                        return Json("مجاز به آپلود این نوع فایل نیستید");

                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = Request.Files[0].FileName.Split(new char[] { '\\' });
                        fileName = testfiles[testfiles.Length - 1];
                    }
                    else
                        fileName = Request.Files[0].FileName;

                    // Change file name to guid
                    string extension = Path.GetExtension(fileName);
                    fileName = Guid.NewGuid().ToString() + extension;

                    // Set Path  
                    if (FileHelper.ExistingFolder(Enum.GetName(typeof(PathType), type)))
                        filePath = Path.Combine(Server.MapPath($"~/content/TemporaryFiles/{Enum.GetName(typeof(PathType), type)}"), fileName);

                    // Save the file
                    HttpPostedFileBase file = Request.Files[0];
                    file.SaveAs(filePath);
                }

                return Json(new { Success = true, Data = new { FileName = fileName } });
            }
            catch (Exception e)
            {
                return Json(new { Success = false, Data = new { } });
            }
        }
        [HttpPost]
        public virtual ActionResult Remove(DeleteAttachmentVM model)
        {

            try
            {
                if (model.ID != null || model.ID != Guid.Empty)
                {
                    _service.Delete(model.ID);
                }
                string filePath = Server.MapPath($@"~/content/TemporaryFiles/{Enum.GetName(typeof(PathType), model.PathType)}/{model.FileName}");
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                return Json(new { Success = true, Data = true });
            }
            catch (Exception e)
            {
                return Json(new { Success = false, Data = false });
            }
        }

        [HttpPost]
        public virtual JsonResult Add(List<FM.Portal.Core.Model.Attachment> model)
        {
            try
            {
                for (int i = 0; i < model.Count; i++)
                {
                    byte[] bytes = System.IO.File.ReadAllBytes(Server.MapPath($"~/content/TemporaryFiles/{Enum.GetName(typeof(PathType), model[i].PathType)}/" + model[i].FileName));
                    model[i].Data = bytes;
                    var result = _service.Add(model[i]);
                }
                return Json(new { Success = true });
            }
            catch (Exception e) { throw; }
        }

        [HttpPost]
        public virtual JsonResult Update(FM.Portal.Core.Model.Attachment model)
        {
            try
            {
                var result = _service.Update(model);
                return Json(result);
            }
            catch { throw; }
        }

        [HttpPost]
        public virtual JsonResult Get(Guid ID)
        {
            try
            {
                var result = _service.Get(ID);
                return Json(result);
            }
            catch { throw; }
        }

        [HttpPost]
        public virtual JsonResult List(Guid ParentID)
        {
            try
            {
                var result = _service.List(ParentID);
                return Json(result);
            }
            catch { throw; }
        }
    }
}
