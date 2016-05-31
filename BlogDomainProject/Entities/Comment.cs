using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using BlogDomainProject.Utils;
using BlogDomainProject.Interface;

namespace BlogDomainProject.Entities
{
    public class Comment : IEntity
    { 
        [Key]
        [Column("comment_id", TypeName = "int")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the comment body.")]
        [StringLength(500, ErrorMessage = "Comment body must not be more than 500 characters.")]
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

        [ForeignKey("Blog")]
        [Column("blog_id", TypeName = "int")]
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        [ForeignKey("Poster")]
        [Column("poster_user_id", TypeName = "int")]
        public int PosterId { get; set; }
        public virtual User Poster { get; set; } 
    }
    
    public class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            ToTable("comments");

            Property(b => b.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 

            HasRequired(c => c.Poster)
                .WithMany(p => p.Comments)
                .HasForeignKey(k => k.PosterId);

            HasRequired(c => c.Blog)
                .WithMany(p => p.Comments)
                .HasForeignKey(k => k.BlogId);
        }
    }
}
