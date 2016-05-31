using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using BlogDomainProject.Utils;

namespace BlogDomainProject.Entities
{
    public class Author
    {
        public Author()
        {
            Blogs = new HashSet<Blog>();
            Categories = new HashSet<Category>();
        }

        [Key, ForeignKey("User")]
        [Column("user_id", TypeName = "int")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter the name of the author.")]
        [StringLength(50, ErrorMessage = "Name of authors should not be more than 50 characters.")]
        [Column("name", TypeName = "nvarchar")]
        public string Name { get; set; }

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
         
        public virtual User User { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Category> Categories { get; set; } 
    }


    public class AuthorConfiguration : EntityTypeConfiguration<Author>
    {
        public AuthorConfiguration()
        {
            ToTable("authors");
            Property(a => a.UserId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None); 
        }        
    }
}
