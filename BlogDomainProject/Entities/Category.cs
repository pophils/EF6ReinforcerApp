using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using BlogDomainProject.Utils;

namespace BlogDomainProject.Entities
{
    public class Category : IValidatableObject
    {
        public Category()
        {
            Blogs = new HashSet<Blog>();
        }

        [Key]
        [Column("category_id", TypeName = "int")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a category name")]
        [StringLength(20, ErrorMessage = "Please enter a category name that is not more than 20 characters")]
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

        [Column("author_user_id", TypeName = "int")]
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            using (var dbContext = new DomainDBContext())
            {
                var categoryExist = dbContext.Categories.Any(c => c.Name.ToLower() == Name.ToLower());

                if (categoryExist)
                {
                    yield return new ValidationResult("This category already exist, please enter a new name.",
                        new string []{"Name"});
                }
            }
        }
    }

    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            ToTable("categories");

            Property(b => b.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(c => c.Author)
                .WithMany(a => a.Categories)
                .HasForeignKey(k => k.AuthorId)
                .WillCascadeOnDelete(false); // do this because Category has one to many rel with blogs, so setting this to true will throw exception.
        }
    }
}
