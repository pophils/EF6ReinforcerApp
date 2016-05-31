using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using BlogDomainProject.Utils;
using BlogDomainProject.Interface;

namespace BlogDomainProject.Entities
{
    public class User : IValidatableObject, IEntity
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Roles = new HashSet<Role>();
        }

        [Key]
        [Column("user_id", TypeName = "int")]
        public int Id { get; set; }

        [Required(ErrorMessage="Please enter a valid email address")]
        [StringLength(20, ErrorMessage="Please enter an email address that is not more than 20 characters")]
        [Column("email", TypeName = "nvarchar")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [StringLength(20, ErrorMessage = "Please enter a password that is not more than 20 characters")]
        [MinLength(6, ErrorMessage="Password must not be less than 6 characters")]
        [Column("password", TypeName = "nvarchar")]
        public string Password { get; set; }
         
        [Column("user_guid", TypeName = "nvarchar")]
        public string UserGuid { get; set; }


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

        [Timestamp] 
        public byte[] RowVersion { get; set; }


        [Column("role_id", TypeName = "int")]
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        public virtual Author Author { get; set; }

        public virtual Reader Reader { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            using (var dbContext = new DomainDBContext())
            {
                var userExist = dbContext.Users.Any(u => u.Email.ToLower() == Email.ToLower());

                if (userExist)
                {
                    yield return new ValidationResult("The entered email already exist, please enter a new email.",
                        new string[] { "Email" });
                }
            }
        }
         
    }


    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("users");
            Property(u => u.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(k => k.RoleId);

           // HasOptional(u => u.Author)
           //     .WithRequired(a => a.User);

           // HasOptional(u => u.Reader)
           //.WithRequired(a => a.User);
        }         
    }
}
