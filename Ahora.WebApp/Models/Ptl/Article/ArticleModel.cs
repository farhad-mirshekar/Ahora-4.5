using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace Ahora.WebApp.Models.Ptl
{
    public class ArticleModel
    {
        public Guid ID { get; set; }
        public string Path { get; }
        public Guid? LanguageID { get; set; }
        public string ReadingTime { get; set; }
        public PathType PathType { get; set; }
        public string FileName { get; set; }
        public string CreatorName { get; set; }
        public List<string> Tags { get; set; }
        public string TrackingCode { get; set; }
        public Guid? RemoverID { get; set; }
        public Guid UserID { get; set; }
        public Guid CategoryID { get; set; }
        public ViewStatusType ViewStatusType { get; set; }
        public string UrlDesc { get; set; }
        public int DisLikeCount { get; set; }
        public int LikeCount { get; set; }
        public int VisitedCount { get; set; }
        public CommentStatusType CommentStatusType { get; set; }
        public string Description { get; set; }
        public string MetaKeywords { get; set; }
        public string Body { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Title { get; set; }
        public int Total { get; set; }
        public DateTime? CreationDate { get; set; }
        public List<Attachment> PictureAttachments { get; set; }
        public List<Attachment> VideoAttachments { get; set; }
    }
}