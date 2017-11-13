using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbracoFilterGrid.Model
{
    public class ContentModel
    {
        public string Title { get; set; }
        public string Descrption { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }
}
