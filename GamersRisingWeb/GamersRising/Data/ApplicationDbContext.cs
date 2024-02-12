using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GamersRising.Entities;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace GamersRising.Data
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(250)]
        public string FullName { get; set; }

        [StringLength(250)]
        public string DateOfBirth { get; set; }



        [ForeignKey("UserId")]
        public virtual ICollection<UserCatagory>? UserCategory { get; set; }

    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Game> Game { get; set; }
        public DbSet<Tournament> Tournament { get; set; }
        public DbSet<TournamentType> TournamentType { get; set; }
        public DbSet<UserCatagory> UserCatagory { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<TournamentFormat> TournamentFormat { get; set; } 
        public DbSet<TournamentMode> TournamentMode { get; set; }
    }
}