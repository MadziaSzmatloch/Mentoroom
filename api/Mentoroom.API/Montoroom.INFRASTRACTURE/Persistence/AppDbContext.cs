using Mentoroom.DOMAIN.Entities.LecturerModels;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Entities.StudentModels;
using Mentoroom.DOMAIN.Entities.Tags;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mentoroom.INFRASTRACTURE.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseAssignment> Assignments { get; set; }
        public DbSet<AssignmentFile> AssignmentFiles { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<StudentAssignment> StudentAssignments { get; set; }
        public DbSet<StudentAssignmentFile> StudentAssignmentFiles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<AccessCode> AccessCodes { get; set; }
        public DbSet<CourseCoAuthor> CourseCoAuthors { get; set; }
        public DbSet<CourseTags> CourseTags { get; set; }
        public DbSet<AssignmentAttachment> AssigmnentAttachments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Student>()
                .HasKey(s => new { s.UserId });

            builder.Entity<CourseCoAuthor>()
                .HasKey(cca => new { cca.CourseId, cca.CoAuthorId });
            builder.Entity<CourseCoAuthor>()
                .HasOne(x => x.CoAuthor)
                .WithMany()
                .HasForeignKey(x => x.CoAuthorId);


            base.OnModelCreating(builder);
        }
    }
}
