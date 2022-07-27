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


            modelBuilder.Entity<Category>()
                .Property(x => x.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<ServiceComment>()
                .Property(x => x.Title)
                .HasMaxLength(50);

            modelBuilder.Entity<ServiceComment>()
                .Property(x => x.Description)
                .HasMaxLength(2000);

            modelBuilder.Entity<Status>()
                .Property(x => x.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<PhysicalFile>()
                .Property(x => x.Path)
                .HasMaxLength(500);

            #region Order

            modelBuilder.Entity<Order>()
                .Property(x => x.Description)
                .IsRequired(false)
                .HasMaxLength(2000);

            modelBuilder.Entity<Order>()
                .Property(x => x.FinalPrice)
                .IsRequired(false);

            //--------------------------------------------------
            modelBuilder.Entity<Order>()
                .HasOne(x => x.Service)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.ServiceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ServiceComment>()
                .HasOne(x => x.Order)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);            

            modelBuilder.Entity<Suggest>()
                .HasOne(x => x.Expert)
                .WithMany(x => x.Suggests)
                .HasForeignKey(x => x.ExpertId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Status)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.StatusId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            //------------------------------------------------------------------------
            modelBuilder.Entity<Order>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.CustomerOrders)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Expert)
                .WithMany(x => x.ExpertOrders)
                .HasForeignKey(x => x.ConfirmedExpertId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
            //--------------------------------------------------------------------------

            #endregion

            modelBuilder.Entity<ExpertCategory>()
                .HasOne(x => x.Expert)
                .WithMany(x => x.ExpertCategories)
                .HasForeignKey(x => x.ExpertId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExpertCategory>()
                .HasOne(x => x.Category)
                .WithMany(x => x.ExpertCategories)
                .HasForeignKey(x => x.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            


            modelBuilder.Entity<Suggest>()
                .HasOne(x => x.Order)
                .WithMany(x => x.ExpertSuggests)
                .HasForeignKey(x => x.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasMany(x => x.ExpertSuggests)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<ServiceComment>()
            //    .HasOne(x => x.Service)
            //    .WithMany(x => x.ServiceComments)
            //    .HasForeignKey(x => x.ServiceId)
            //    .IsRequired(false);





            modelBuilder.Entity<ServiceFile>()
                .HasOne(x => x.Service)
                .WithMany(x => x.ServiceFiles)
                .HasForeignKey(x => x.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ServiceFile>()
                .HasOne(x => x.File)
                .WithMany(x => x.ServiceFiles)
                .HasForeignKey(x => x.FileId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderFile>()
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderFiles)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderFile>()
                .HasOne(x => x.File)
                .WithMany(x => x.OrderFiles)
                .HasForeignKey(x => x.FileId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserFile>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserFiles)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserFile>()
                .HasOne(x => x.File)
                .WithMany(x => x.UserFiles)
                .HasForeignKey(x => x.FileId)
                .OnDelete(DeleteBehavior.Cascade);
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