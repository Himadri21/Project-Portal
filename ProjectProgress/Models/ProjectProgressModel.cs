namespace ProjectProgress.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ProjectProgressModel : DbContext
    {
        public ProjectProgressModel()
            : base("name=PPModel")
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Mark> Marks { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .Property(e => e.DepartmentName)
                .IsFixedLength();

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Teachers)
                .WithOptional(e => e.Department)
                .HasForeignKey(e => e.DepartmentID);

            modelBuilder.Entity<Project>()
                .Property(e => e.ProjectTitle)
                .IsFixedLength();

            modelBuilder.Entity<Project>()
                .Property(e => e.ProjectGenre)
                .IsFixedLength();

            modelBuilder.Entity<Project>()
                .Property(e => e.Report)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.StudentName)
                .IsFixedLength();

            modelBuilder.Entity<Student>()
                .HasOptional(e => e.Mark)
                .WithRequired(e => e.Student);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.TeacherName)
                .IsFixedLength();

            modelBuilder.Entity<Teacher>()
                .Property(e => e.TeacherDomain)
                .IsFixedLength();

            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.Departments)
                .WithOptional(e => e.Teacher)
                .HasForeignKey(e => e.HoD_Id);
        }
    }
}
