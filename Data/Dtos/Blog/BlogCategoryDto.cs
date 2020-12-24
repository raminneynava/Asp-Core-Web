using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class BlogCategoryDto
    {
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ICollection<BlogDto> Blogs { get; set; }
    }
}
