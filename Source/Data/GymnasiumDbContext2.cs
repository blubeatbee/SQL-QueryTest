using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Source.Models.Gym2;

namespace Source.Data
{
	public partial class GymnasiumDbContext2 : DbContext
	{
		public GymnasiumDbContext2()
		{
		}

		public GymnasiumDbContext2(DbContextOptions<GymnasiumDbContext2> options)
			: base(options)
		{
		}

		public virtual DbSet<Class> Classes { get; set; }
		public virtual DbSet<Course> Courses { get; set; }
		public virtual DbSet<Employee> Employees { get; set; }
		public virtual DbSet<Grading> Gradings { get; set; }
		public virtual DbSet<Role> Roles { get; set; }
		public virtual DbSet<Student> Students { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				.AddJsonFile("appsettings.json")
				.Build();
			optionsBuilder.UseSqlServer(config.GetConnectionString("GymnasiumConnection2"));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Class>(entity =>
			{
				entity.ToTable("Class");

				entity.HasKey(e => e.ClassId).HasName("PK_Class_ClassId");
				entity.HasIndex(e => e.ClassId, "UQ_Class_ClassId").IsUnique();

				entity.Property(e => e.ClassId)
					.HasMaxLength(7).IsFixedLength().IsRequired();
				entity.Property(e => e.DateStart)
					.HasColumnType("Date").IsRequired();
				entity.Property(e => e.DateEnd)
					.HasColumnType("Date").IsRequired();
			});

			modelBuilder.Entity<Course>(entity =>
			{
				entity.ToTable("Course");

				entity.HasKey(e => e.CourseId).HasName("PK_Course_CourseId");

				entity.Property(e => e.CourseId).UseIdentityColumn(1, 1);
				entity.Property(e => e.ClassId)
					.HasMaxLength(7).IsFixedLength();
				entity.Property(e => e.Title)
					.HasMaxLength(50).IsRequired();
				entity.Property(e => e.DateStart)
					.HasColumnType("DATE").IsRequired();
				entity.Property(e => e.DateEnd)
					.HasColumnType("DATE").IsRequired();

				entity.HasOne(c => c.Class).WithMany(c => c.Courses)
					.HasForeignKey(c => c.ClassId)
					.OnDelete(DeleteBehavior.Cascade)
					.HasConstraintName("FK_Course_Class");
			});

			modelBuilder.Entity<Employee>(entity =>
			{
				entity.ToTable("Employee");
				entity.HasKey(e => e.EmployeeId).HasName("PK_Employee_EmployeeId");

				entity.Property(e => e.EmployeeId).UseIdentityColumn(1, 1);
				entity.Property(e => e.Ssn)
					.HasColumnName("SSN")
					.HasMaxLength(12)
					.IsFixedLength()
					.IsRequired();
				entity.Property(e => e.Surname)
					.HasMaxLength(50).IsRequired();
				entity.Property(e => e.Name)
					.HasMaxLength(50).IsRequired();
				entity.Property(e => e.Tasks).HasMaxLength(500);
				entity.Property(e => e.DateHired)
					.HasColumnType("DATE").IsRequired();
				entity.Property(e => e.DateQuit).HasColumnType("DATE");
				entity.Property(e => e.IsEmployed).IsRequired();

				entity.HasOne(e => e.Role).WithMany(e => e.Employees)
					.HasForeignKey(e => e.RoleId)
					.OnDelete(DeleteBehavior.NoAction)
					.HasConstraintName("FK_Employee_ERole");
			});

			modelBuilder.Entity<Grading>(entity =>
			{
				entity.ToTable("Grading");
				entity.HasKey(e => e.GradingId).HasName("PK_Grading_GradingId");

				entity.Property(e => e.Grade).IsRequired();
				entity.Property(e => e.DateSet)
					.HasColumnType("DATE").IsRequired();

				entity.HasOne(e => e.Course).WithMany(e => e.Gradings)
					.HasForeignKey(e => e.CourseId)
					.OnDelete(DeleteBehavior.NoAction)
					.HasConstraintName("FK_Grading_Course");
				entity.HasOne(e => e.Teacher).WithMany(e => e.Gradings)
					.HasForeignKey(e => e.TeacherId)
					.OnDelete(DeleteBehavior.NoAction)
					.HasConstraintName("FK_Grading_Teacher");
				entity.HasOne(e => e.Student).WithMany(e => e.Gradings)
					.HasForeignKey(e => e.StudentId)
					.OnDelete(DeleteBehavior.NoAction)
					.HasConstraintName("FK_Grading_Student");
			});

			modelBuilder.Entity<Role>(entity =>
			{
				entity.ToTable("ERole");
				entity.HasKey(e => e.RoleId).HasName("PK_ERole");
				entity.HasIndex(e => e.RoleTitle, "UQ_ERole_RoleTitle").IsUnique();

				entity.Property(e => e.RoleId).UseIdentityColumn(1, 1);
				entity.Property(e => e.RoleTitle)
					.HasMaxLength(50).IsRequired();
			});

			modelBuilder.Entity<Student>(entity =>
			{
				entity.ToTable("Student");
				entity.HasKey(e => e.StudentId).HasName("PK_Student_StudentId");

				entity.Property(e => e.StudentId)
					.UseIdentityColumn(1, 1).IsRequired();
				entity.Property(e => e.ClassId)
					.HasMaxLength(7).IsFixedLength();
				entity.Property(e => e.Ssn)
					.HasColumnName("SSN")
					.HasMaxLength(12)
					.IsFixedLength()
					.IsRequired();
				entity.Property(e => e.Surname)
					.HasMaxLength(50).IsRequired();
				entity.Property(e => e.Name)
					.HasMaxLength(50).IsRequired();
				entity.Property(e => e.DateEnrolled)
					.HasColumnType("DATE").IsRequired();
				entity.Property(e => e.DateQuit).HasColumnType("DATE");
				entity.Property(e => e.IsActive).IsRequired();

				entity.HasOne(e => e.Class).WithMany(e => e.Students)
					.HasForeignKey()
					.OnDelete(DeleteBehavior.NoAction)
					.HasConstraintName("FK_Student_Class");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
