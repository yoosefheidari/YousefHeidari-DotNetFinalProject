using App.Domain.Core.BaseData.Entities;
using App.Domain.Core.User.Entities;
using App.Domain.Core.Work.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.DataBase.SqlServer
{
    public class AppDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
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
                .IsRequired(false);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Service)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.ServiceId)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.CustomerOrders)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();            

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Expert)
                .WithMany(x => x.ExpertOrders)
                .HasForeignKey(x => x.ConfirmedExpertId)
                .IsRequired(false);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Status)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.StatusId)
                .IsRequired();

            #endregion


            modelBuilder.Entity<Suggest>()
                .HasOne(x => x.Expert)
                .WithMany(x => x.Suggests)
                .HasForeignKey(x => x.ExpertId)
                .IsRequired();

            modelBuilder.Entity<Suggest>()
                .HasOne(x => x.Order)
                .WithMany(x => x.ExpertSuggests)
                .HasForeignKey(x => x.OrderId)
                .IsRequired(false);

            


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
                .Property(x => x.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<ServiceComment>()
                .Property(x => x.Title)
                .HasMaxLength(50);

            modelBuilder.Entity<ServiceComment>()
                .Property(x => x.Description)
                .HasMaxLength(2000);
            modelBuilder.Entity<ServiceComment>()
                .HasOne(x => x.Service)
                .WithMany(x => x.ServiceComments)
                .HasForeignKey(x => x.ServiceId)
                .IsRequired(false);


            modelBuilder.Entity<Status>()
                .Property(x => x.Name)
                .HasMaxLength(50);


            modelBuilder.Entity<ServiceFile>()
                .HasOne(x => x.Service)
                .WithMany(x => x.ServiceFiles)
                .HasForeignKey(x => x.ServiceId);

            modelBuilder.Entity<ServiceFile>()
                .HasOne(x => x.File)
                .WithMany(x => x.ServiceFiles)
                .HasForeignKey(x => x.FileId);

            modelBuilder.Entity<OrderFile>()
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderFiles)
                .HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<OrderFile>()
                .HasOne(x => x.File)
                .WithMany(x => x.OrderFiles)
                .HasForeignKey(x => x.FileId);

            modelBuilder.Entity<UserFile>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserFiles)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserFile>()
                .HasOne(x => x.File)
                .WithMany(x => x.UserFiles)
                .HasForeignKey(x => x.FileId);


            modelBuilder.Entity<PhysicalFile>()
                .Property(x => x.Path)
                .HasMaxLength(500);

        }
        #region User Aggregate DbSets
        public virtual DbSet<AppUser> AppUsers { get; set; } = null!;
        public virtual DbSet<UserFile> UserFiles { get; set; } = null!;
        #endregion

        #region BaseData Aggregate Dbsets
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<PhysicalFile> Files { get; set; } = null!;
        #endregion

        #region MainWork Aggreagate Dbsets
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<ExpertCategory> ExpertCategories { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderFile> OrderFiles { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<ServiceComment> Comments { get; set; } = null!;
        public virtual DbSet<ServiceFile> ServiceFiles { get; set; } = null!;
        public virtual DbSet<Suggest> Suggests { get; set; } = null!;
        #endregion
    }
}