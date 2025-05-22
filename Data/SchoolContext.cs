using HelloAspMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloAspMVC.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Students");
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(
                "Server=localhost;Database=test;User=root;Password=ldcc!2626;", // 연결 문자열 수정
                new MySqlServerVersion(new Version(10, 5, 10)) // MariaDB 버전 명시 (필요시)
            );
            
        }
    }
    
    
}