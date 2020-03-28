using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class CommentController : BaseController<ICommentService>
    {
        private readonly IProductService _productService;
        private readonly ICommentMapUserService _commentMapuserService;
        public CommentController(ICommentService service
                                , IProductService productService
                                , ICommentMapUserService commentMapuserService) : base(service)
        {
            _productService = productService;
            _commentMapuserService = commentMapuserService;
        }

        // GET: Comment
        public ActionResult Index(Guid DocumentID,string State)
        {
            switch (State)
            {
                case "Product":
                    {
                        var product = _productService.Get(DocumentID);
                        if (product.Success)
                        {
                            ViewBag.stateComment = product.Data.AllowCustomerReviews;
                        }
                        break;
                    }
            }
            var comment = _service.List(new CommentListVM {DocumentID=DocumentID });
            ViewBag.user = HttpContext.User.Identity.Name;
            ViewBag.DocumentID = DocumentID;
            return PartialView("_PartialComment",comment.Data);
        }
        [HttpGet]
        public JsonResult CommentReply(Guid ParentID)
        {
            return Json(new {status=1 }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Modify(Guid DocumentID, Guid? ParentID)
        {
            if(ParentID.HasValue)
                ViewBag.ParentID = ParentID.Value;
            return PartialView("~/Views/Comment/_PartialModify.cshtml");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Modify(Comment model)
        {
            if (SQLHelper.CheckGuidNull(User.Identity.Name) == Guid.Empty)
                return Json(new {show="true" });
            model.UserID = SQLHelper.CheckGuidNull(User.Identity.Name);
            model.CommentType = CommentType.در_حال_بررسی;
        
            _service.Add(model);
            return Json(new { show = "false" });
        }

        public JsonResult Like(Guid CommentID)
        {
            string result = "";
            int count = 0;
            if (SQLHelper.CheckGuidNull(User.Identity.Name) == null)
                return Json(new { result = "login", CommentID = CommentID });

            var isUserLike = _commentMapuserService.IsUserLike(new CommentMapUser {CommentID=CommentID,UserID= SQLHelper.CheckGuidNull(User.Identity.Name) });
            if (isUserLike.Data)
            {
                _commentMapuserService.Add(new CommentMapUser { CommentID = CommentID, UserID = SQLHelper.CheckGuidNull(User.Identity.Name) });
                var resultLike= _service.Like(CommentID);
                count = resultLike.Data;
                result = "success";
            }
            else
                result = "duplicate";
            return Json(new { result, CommentID = CommentID,count=count, state = "like" });

        }
        public JsonResult DisLike(Guid CommentID)
        {
            string result = "";
            int count = 0;
            if (SQLHelper.CheckGuidNull(User.Identity.Name) == null || SQLHelper.CheckGuidNull(User.Identity.Name) == Guid.Empty)
                return Json(new { result = "login", CommentID = CommentID });

            var isUserLike = _commentMapuserService.IsUserLike(new CommentMapUser { CommentID = CommentID, UserID = SQLHelper.CheckGuidNull(User.Identity.Name) });
            if (isUserLike.Data)
            {
                _commentMapuserService.Add(new CommentMapUser { CommentID = CommentID, UserID = SQLHelper.CheckGuidNull(User.Identity.Name) });
                var resultLike = _service.DisLike(CommentID);
                count = resultLike.Data;
                result = "success";
            }
            else
                result = "duplicate";
            return Json(new { result, CommentID = CommentID, state = "dislike", count = count });

        }
    }
}