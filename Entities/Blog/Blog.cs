using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
     public class Blog:BaseEntity
    {
        public string Title { get; set; }
        public string Abstract { get; set; }
        public int BlogCategoryID { get; set; }
        [ForeignKey("BlogCategoryID")]
        public BlogCategory BlogCategory { get; set; }

     
    }
}
