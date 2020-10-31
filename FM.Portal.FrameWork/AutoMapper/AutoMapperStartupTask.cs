using Ahora.WebApp.Models.Pbl.Language;
using Ahora.WebApp.Models.Pbl.Menu;
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

                config.CreateMap<MenuVM, MemuModel>();
            });
        }
    }
}
