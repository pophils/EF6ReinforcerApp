using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using BlogDomainProject.Utils;

namespace BlogDomainProject.Entities
{
    public class Role : IValidatableObject
    {
        public Role()
        {
            Users = new HashSet<User>(); 
        }

        [Key]
        [Column("role_id", TypeName = "int")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a role name")]
        [StringLength(20, ErrorMessage = "Please enter a role name that is not more than 20 characters")]
        [Column("name", TypeName = "nvarchar")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a the created date")]
        [Column("created_date", TypeName = "DateTime2")]
        public DateTime CreatedDate { get; set; }

        [Column("last_modified_date", TypeName = "DateTime2")]
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

        public virtual ICollection<User> Users { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            using (var dbContext = new DomainDBContext())
            {
                var roleExist = dbContext.Roles.Any(r => r.Name.ToLower() == Name.ToLower());

                if (roleExist)
                {
                    yield return new ValidationResult("This role already exist, please enter a new name.",
                        new string[] { "Name" });
                }
            }
        }
    }
    
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            ToTable("roles");
            Property(r => r.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);             
        }         
    }
}
