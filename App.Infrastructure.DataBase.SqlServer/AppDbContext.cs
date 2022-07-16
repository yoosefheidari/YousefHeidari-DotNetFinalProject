using App.Domain.Core.BaseData.Entities;
using App.Domain.Core.Operator.Entities;
using App.Domain.Core.Work.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.DataBase.SqlServer
{
    public class AppDbContext : IdentityDbContext<IdentityUser<int>,IdentityRole<int>,int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Order
            modelBuilder.Entity<Order>()
                .Property(x => x.Description)
                .IsRequired(false)
                .HasMaxLength(2000);
            modelBuilder.Entity<Order>()
                .Property(x => x.FinalPrice)
                .IsRequired(false)
                .HasPrecision(10,0);
            modelBuilder.Entity<Order>()
                .Property(x => x.SuggestedWorkTimeByExpert)
                .IsRequired(false)
                .HasMaxLength(250);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CategoryId)
                .IsRequired();          

            modelBuilder.Entity<Order>()                
                .HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Expert)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.ConfirmedExpertId)
                .IsRequired(false);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Status)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.StatusId)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .HasMany(x => x.OrderTags)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .IsRequired();
            #endregion

            modelBuilder.Entity<Tag>()
                .HasMany(x => x.OrderTags)
                .WithOne(x => x.Tag)
                .HasForeignKey(x => x.TagId)
                .IsRequired();

            modelBuilder.Entity<Tag>()
                .HasOne(x => x.TagGroup)
                .WithMany(x => x.Tags)
                .HasForeignKey(x => x.TagGroupId)
                .IsRequired();

            modelBuilder.Entity<Tag>()
                .Property(x => x.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<TagGroup>()
                .Property(x => x.Name)
                .HasMaxLength(50);



            modelBuilder.Entity<ExpertSuggest>()
                .HasOne(x => x.Expert)
                .WithMany(x => x.ExpertSuggests)
                .HasForeignKey(x => x.ExpertId)
                .IsRequired();

            modelBuilder.Entity<ExpertSuggest>()
                .HasOne(x => x.Order)
                .WithMany(x => x.ExpertSuggests)
                .HasForeignKey(x => x.OrderId)
                .IsRequired(false);
          
            modelBuilder.Entity<ExpertSuggest>()
                .Property(x => x.Price)
                .HasPrecision(10,0);


            modelBuilder.Entity<ExpertCategory>()
                .HasOne(x => x.Expert)
                .WithMany(x => x.ExpertCategories)
                .HasForeignKey(x => x.ExpertId)
                .IsRequired();

            modelBuilder.Entity<ExpertCategory>()
                .HasOne(x => x.Category)
                .WithMany(x => x.ExpertCategories)
                .HasForeignKey(x => x.CategoryId)
                .IsRequired();

            
            modelBuilder.Entity<Category>()
                .Property(x => x.DisplayOrder)
                .IsRequired(false);
            modelBuilder.Entity<Category>()
                .Property(x => x.DisplayOrder)
                .IsRequired(false);
            modelBuilder.Entity<Category>()
                .Property(x => x.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<Comment>()
                .Property(x => x.Title)
                .HasMaxLength(50);
            modelBuilder.Entity<Comment>()
                .Property(x => x.Title)
                .HasMaxLength(2000);


            modelBuilder.Entity<Status>()
                .Property(x => x.Name)
                .HasMaxLength(50);



            modelBuilder.Entity<CategoryTagGroup>()
                .HasOne(x => x.Category)
                .WithMany(x => x.CategoryTagGroups)
                .HasForeignKey(x => x.CategoryId)
                .IsRequired();

            modelBuilder.Entity<CategoryTagGroup>()
                .HasOne(x => x.TagGroup)
                .WithMany(x => x.CategoryTagGroups)
                .HasForeignKey(x => x.TagGroupId)
                .IsRequired();

            modelBuilder.Entity<PhysicalFile>()
                .HasOne(x => x.Expert)
                .WithMany(x => x.PhysicalFiles)
                .HasForeignKey(x => x.ExpertId)
                .IsRequired(false);

            modelBuilder.Entity<PhysicalFile>()
                .HasOne(x => x.Order)
                .WithMany(x => x.PhysicalFiles)
                .HasForeignKey(x => x.OrderId)
                .IsRequired(false);

            modelBuilder.Entity<PhysicalFile>()
                .Property(x => x.Path)
                .HasMaxLength(500);
        }
        #region User Aggregate DbSets
        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Expert> Experts { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        #endregion

        #region BaseData Aggregate Dbsets
        public virtual DbSet<Status> Statuses { get; set; } = null!;        
        public virtual DbSet<PhysicalFile> Files { get; set; } = null!;
        #endregion

        #region MainWork Aggreagate Dbsets
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<OrderTag> OrderTags { get; set; } = null!;
        public virtual DbSet<CategoryTagGroup> CategorySpecifications { get; set; } = null!;
        public virtual DbSet<TagGroup> TagGroups { get; set; } = null!;
        public virtual DbSet<ExpertCategory> ExpertCategories { get; set; } = null!;
        public virtual DbSet<CategoryTagGroup> CategoryTagGroups { get; set; } = null!;
        public virtual DbSet<ExpertSuggest> ExpertSuggests { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        #endregion
    }
}