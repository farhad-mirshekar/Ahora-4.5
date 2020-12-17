using Ahora.WebApp.Models.App;
using Ahora.WebApp.Models.Org;
using Ahora.WebApp.Models.Pbl;
using Ahora.WebApp.Models.Ptl;
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

                config.CreateMap<Product, ProductModel>();
                config.CreateMap<RegisterModel, User>();

                config.CreateMap<Slider,SliderModel>();
            });
        }
    }
}
