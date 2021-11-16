using Group_Guide.Data.Dtos.Auth;
using Group_Guide.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Configuration;

namespace Group_Guide.Data
{
    public class GroupGuideContext : IdentityDbContext<GroupGuideUser>
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // !!! DON'T STORE THE REAL CONNECTION STRING THE IN PUBLIC REPO !!!
            // Use secret managers provided by your chosen cloud provider
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=GroupGuide");

        }
    }
}
