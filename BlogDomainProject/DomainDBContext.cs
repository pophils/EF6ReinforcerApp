using System.Data.Entity;
using BlogDomainProject.Entities;
using BlogDomainProject.Migrations;

namespace BlogDomainProject
{
    public class DomainDBContext : DbContext
    {
        public DomainDBContext()
            : base("name=blog_connection")
        {
            Database.SetInitializer<DomainDBContext>(new MigrateDatabaseToLatestVersion<DomainDBContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Configurations.Add(new AuthorConfiguration());
            modelBuilder.Configurations.Add(new BlogConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new CommentConfiguration());
            modelBuilder.Configurations.Add(new ReaderConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());       
          
          
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        //override savechanges
        public override int SaveChanges()
        {            
            return base.SaveChanges();
        }

    }
}
