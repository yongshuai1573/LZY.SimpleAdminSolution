using System;
using System.Collections.Generic;
using System.Text;

namespace LZY.Model.ViewModels
{
    public class MenuLayerViewModel
    {
        public string tag { get; set; }

        public int id { get; set; }
        public string text { get; set; }
        public int pid { get; set; }

        public string icon { get; set; }
        public string title { get; set; }
        public string url { get; set; }

        public int sort { get; set; } = 0;
        public List<MenuLayerViewModel> childList { get; set; } = new List<MenuLayerViewModel>();

        public bool HasChildren
        {
            get
            {
                return childList.Count > 0;
            }
        }
    }
}
