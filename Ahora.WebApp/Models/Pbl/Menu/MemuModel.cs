using FM.Portal.Core.Model;
using System.Collections.Generic;

namespace Ahora.WebApp.Models.Pbl.Menu
{
    public class MemuModel
    {
        public List<MenuItem> AvailableMenu { get; set; }
        public User User { get; set; }
        public bool IsAdmin { get; set; }
    }
}