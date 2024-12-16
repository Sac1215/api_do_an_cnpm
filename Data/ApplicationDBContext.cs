using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_do_an_cnpm.Models;
using api_do_an_cnpm.Models.api_do_an_cnpm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api_do_an_cnpm.data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<House> Houses { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Comment> Comments { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure relationships

            // Mối quan hệ giữa House và AppUser (OwnerHouse)
            modelBuilder.Entity<House>()
                .HasOne(h => h.Owner)  // Mỗi nhà có một chủ nhà
                .WithMany(u => u.OwnedHouses)  // Chủ nhà có thể sở hữu nhiều nhà
                .HasForeignKey(h => h.OwnerId)
                .OnDelete(DeleteBehavior.Cascade); // Ensure delete behavior is set if required

            // Mối quan hệ giữa House và Facility
            modelBuilder.Entity<House>()
                .HasMany(h => h.Facilities)  // Một nhà có thể có nhiều tiện nghi
                .WithOne(f => f.House)  // Một tiện nghi có một nhà
                .HasForeignKey(f => f.HouseId)  // Chỉ định rõ ràng khóa ngoại là HouseId
                .OnDelete(DeleteBehavior.Cascade);  // Set delete behavior (cascade or restrict)

            // Mối quan hệ giữa House và Comment
            modelBuilder.Entity<House>()
                .HasMany(h => h.Comments)  // Một nhà có thể có nhiều bình luận
                .WithOne(c => c.House)
                .HasForeignKey(c => c.HouseId)
                .OnDelete(DeleteBehavior.Cascade);  // Ensure delete behavior if needed

            // Mối quan hệ giữa Comment và AppUser
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.House)
                .WithMany(h => h.Comments)
                .HasForeignKey(c => c.HouseId)
                .OnDelete(DeleteBehavior.Restrict);  // Change to Restrict if Cascade causes issues

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);  // Change to Restrict if Cascade causes issues
                                                     // Set delete behavior (cascade or restrict)

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "505faa31-ba3e-4982-bfae-308614b4da92",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                 new IdentityRole
                 {
                     Id = "505faa31-ba3e-4982-bfae-308614b4da33",
                     Name = "OwnerHouse",
                     NormalizedName = "OWNERHOUSE"
                 },
                new IdentityRole
                {
                    Id = "5e32977f-1f8d-4429-bbf7-518bdcae5bef",
                    Name = "Student",
                    NormalizedName = "STUDENT"
                }
            );
        }

    }
}