using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeraphimEngine.ContentPipeline.Menu
{
    public class MenuData
    {
        public bool OnActionClose { get; set; }

        public string Id { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
        public bool Center { get; set; }
        public List<MenuChoiceData> Choices { get; set; } = new List<MenuChoiceData>();
    }
}
