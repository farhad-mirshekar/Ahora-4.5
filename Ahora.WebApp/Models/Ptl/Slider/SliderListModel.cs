using System.Collections.Generic;

namespace Ahora.WebApp.Models.Ptl
{
    public class SliderListModel
    {
        public SliderListModel()
        {
            AvailableSliders = new List<SliderModel>();
        }
        public List<SliderModel> AvailableSliders { get; set; }
    }
}