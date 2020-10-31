using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace Ahora.WebApp.Models.Pbl.Menu
{
    public class MemuModel
    {
        public List<MenuVM> AvailableMenu { get; set; }
        public User User { get; set; }
        public bool IsAdmin { get; set; }
    }
}