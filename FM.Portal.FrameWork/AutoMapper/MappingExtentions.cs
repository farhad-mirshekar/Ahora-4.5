using Ahora.WebApp.Models.App;
using Ahora.WebApp.Models.Pbl;
using Ahora.WebApp.Models.Ptl;
using AutoMapper;
using FM.Portal.Core.Model;

namespace FM.Portal.FrameWork.AutoMapper
{
   public static class MappingExtentions
    {
        #region Language
        public static LanguageModel ToModel(this Language entity)
        {
            return Mapper.Map<Language, LanguageModel>(entity);
        }
        public static Language ToEntity(this LanguageModel model)
        {
            return Mapper.Map<LanguageModel, Language>(model);
        }
        #endregion

        #region Menu
        public static MemuModel ToModel(this MenuItem entity)
        {
            return Mapper.Map<MenuItem, MemuModel>(entity);
        }
        #endregion

        #region Events
        public static EventModel ToModel(this Events entity)
        {
            return Mapper.Map<Events, EventModel>(entity);
        }
        #endregion

        #region Articles
        public static ArticleModel ToModel(this Article entity)
        {
            return Mapper.Map<Article, ArticleModel>(entity);
        }
        #endregion

        #region News
        public static NewsModel ToModel(this News entity)
        {
            return Mapper.Map<News, NewsModel>(entity);
        }
        #endregion

        #region ArticleComment
        public static ArticleComment ToEntity(this ArticleCommentModel model)
        {
            return Mapper.Map<ArticleCommentModel, ArticleComment>(model);
        }
        #endregion

        #region NewsComment
        public static NewsComment ToEntity(this NewsCommentModel model)
        {
            return Mapper.Map<NewsCommentModel, NewsComment>(model);
        }
        #endregion

        #region EventsComment
        public static EventsComment ToEntity(this EventsCommentModel model)
        {
            return Mapper.Map<EventsCommentModel, EventsComment>(model);
        }
        #endregion

        #region ProductComment
        public static ProductComment ToEntity(this ProductCommentModel model)
        {
            return Mapper.Map<ProductCommentModel, ProductComment>(model);
        }
        #endregion

        #region Product
        public static ProductModel ToModel(this Product entity)
        {
            return Mapper.Map<Product, ProductModel>(entity);
        }
        #endregion
    }
}
