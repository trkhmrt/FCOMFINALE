using System;
using System.Data;
using System.Reflection.Emit;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Concrete
{

    public class Context : IdentityDbContext<User, Role, Guid>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = localhost; Database = FuturecomClient; Uid = SA; Password =reallyStrongPwd123;TrustServerCertificate=true;");
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Benzersiz GUID'ler oluştur
            Guid adminRoleId = Guid.NewGuid();
            Guid normalUserRoleid = Guid.NewGuid();
            Guid adminUserId = Guid.NewGuid();
            Guid normalUserId = Guid.NewGuid();

            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new Role { Id = normalUserRoleid, Name = "NormalUser", NormalizedName = "NORMALUSER" }
            );

            // Hash the password securely before storing it
            var hasher = new PasswordHasher<User>();

            // Seed Admin User
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = adminUserId,
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "trkhamarat@gmail.com",
                    NormalizedEmail = "TRKHAMARAT@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "1234567"),
                    PhoneNumber = "553 769 63 62",
                    FirstName = "Tarik",
                    LastName = "Hamarat",
                    Status = true
                }
            );

            // Seed Normal User
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = normalUserId,
                    UserName = "normaluser",
                    NormalizedUserName = "NORMALUSER",
                    Email = "user@example.com",
                    NormalizedEmail = "USER@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "1234567"),
                    PhoneNumber = "555 555 55 55",
                    FirstName = "Normal",
                    LastName = "User",
                    Status = true
                }
            );

            // Seed User Roles
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId
                },
                new IdentityUserRole<Guid>
                {
                    RoleId = normalUserRoleid,
                    UserId = normalUserId
                }
            );
        }

        public DbSet<TokenUser> TokenUsers { get; set; }
    }
}


