using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
   public class BlogCategory: BaseEntity
    {
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Order { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public ICollection<Blog> Blogs { get; set; }
    }
    //public class BlogConfig : IEntityTypeConfiguration<BlogCategory>
    //{
    //    public void Configure(EntityTypeBuilder<BlogCategory> builder)
    //    {
    //        builder.Property(p => p.Title).IsRequired(true).HasMaxLength(200);
    //    }
    //}
}
