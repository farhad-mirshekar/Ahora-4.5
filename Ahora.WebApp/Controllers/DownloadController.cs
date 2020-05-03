using Ahora.WebApp.Models;
using FM.Portal.Core.Common;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using System;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class DownloadController : BaseController<IDownloadService>
    {
        public DownloadController(IDownloadService service) : base(service)
        {
        }
        [Route("File/{Token}")]
        public ActionResult FileZip(string Token)
        {
            try
            {
                var downloadResult = _service.Get(SQLHelper.CheckGuidNull(Token));
                if (!downloadResult.Success)
                    return View("Error");
                var download = downloadResult.Data;
                if(download.ExpireDate < DateTime.Now)
                {
                    var error = new Error { ClassCss = "alert alert-danger", ErorrDescription = "زمان توکن شما جهت دانلود فایل به پایان رسیده است" };
                    return View("Error", error);
                }
                return File(download.Data, "application/zip",$"{Guid.NewGuid()}.zip");
            }
            catch(Exception e)
            {
                return View("Error");
            }
        }
    }
}