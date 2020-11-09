using Ahora.WebApp.Models.App;
using Ahora.WebApp.Models.Pbl.Language;
using Ahora.WebApp.Models.Pbl.Menu;
using Ahora.WebApp.Models.Ptl.Article;
using Ahora.WebApp.Models.Ptl.ArticleComment;
using Ahora.WebApp.Models.Ptl.Events;
using Ahora.WebApp.Models.Ptl.EventsComment;
using Ahora.WebApp.Models.Ptl.News;
using Ahora.WebApp.Models.Ptl.NewsComment;
using AutoMapper;
using FM.Portal.Core.Infrastructure;
using FM.Portal.Core.Model;

namespace FM.Portal.FrameWork.AutoMapper
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public int Order => 0;

        public void Execute()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Language, LanguageModel>();
                config.CreateMap<LanguageModel, Language>();

                config.CreateMap<MenuItem, MemuModel>();

                config.CreateMap<Events, EventModel>();
                config.CreateMap<News, NewsModel>();
                config.CreateMap<Article, ArticleModel>();
                config.CreateMap<ArticleCommentModel, ArticleComment>();
                config.CreateMap<NewsCommentModel, NewsComment>();
                config.CreateMap<EventsCommentModel, EventsComment>();
                config.CreateMap<ProductCommentModel, ProductComment>();
            });
        }
    }
}
