using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Schedule.Entities;
using Schedule.Models;
using Schedule.Entities;
using Microsoft.Extensions.Hosting;

namespace Schedule.Data
{
	public class ApplicationDbContext : IdentityDbContext<DbUser, DbRole, int>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		public DbSet<Group> Groups { get; set; }
        public DbSet<Timetable> Timetable { get; set; }
        public DbSet<ScheduleModel> ScheduleModels { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

