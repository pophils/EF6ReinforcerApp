using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using BlogDomainProject.Utils;
using BlogDomainProject.Interface;

namespace BlogDomainProject.Entities
{
    public class Blog : IEntity
    {
        public Blog()
        {
            Comments = new HashSet<Comment>();
        }

        [Key]
        [Column("blog_id", TypeName = "int")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a title")]
        [StringLength(20, ErrorMessage = "Please enter a title that is not more than 20 characters")]
        [Column("title", TypeName = "nvarchar")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter a body")]
        [StringLength(500, ErrorMessage = "Please enter a body that is not more than 500 characters")]
        [Column("body", TypeName = "text")]
        public string Body { get; set; }

        [Required(ErrorMessage = "Please enter a the created date")]
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("last_modified_date")]
        public DateTime LastModifiedDate { get; set; }

        [Required(ErrorMessage = "Please enter a the created by")]
        [Column("created_by", TypeName = "nvarchar")]
        [StringLength(50, ErrorMessage = "Please enter a created by that is not more than 50 characters")]
        public string CreatedBy { get; set; }

        [Column("last_modified_by", TypeName = "nvarchar")]
        [StringLength(50, ErrorMessage = "Please enter a last created by that is not more than 50 characters")]
        public string LastModifiedBy { get; set; }

        [NotMapped]
        public EntityStateEnum EntityState { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Column("category_id", TypeName = "int")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Column("author_user_id", TypeName = "int")]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }      
    }

    public class BlogConfiguration : EntityTypeConfiguration<Blog>
    {
        public BlogConfiguration()
        {
            ToTable("blogs");

            Property(b => b.Id) 
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(b => b.Category)
                .WithMany(c => c.Blogs)
                .HasForeignKey(k => k.CategoryId);

            HasRequired(b => b.Author)
               .WithMany(c => c.Blogs)
               .HasForeignKey(k => k.AuthorId);
        }
    }
}
