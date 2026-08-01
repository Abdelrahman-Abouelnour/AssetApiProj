using AssetApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AssetApi.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portofolio> Portofolios { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Portofolio>(x => x.HasKey(p => new {p.AppuserId,p.StockId}));
            builder.Entity<Portofolio>().HasOne(p => p.AppUser).WithMany(u => u.Portofolios).HasForeignKey(p => p.AppuserId);
            builder.Entity<Portofolio>().HasOne(p => p.Stock).WithMany(s => s.Portofolios).HasForeignKey(p => p.StockId);

            List<IdentityRole> roles = new()
            {
                new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "1"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "2"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}