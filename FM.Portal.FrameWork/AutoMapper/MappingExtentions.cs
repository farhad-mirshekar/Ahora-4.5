using Ahora.WebApp.Models.Pbl.Language;
using Ahora.WebApp.Models.Pbl.Menu;
using Ahora.WebApp.Models.Ptl.Events;
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
    }
}
