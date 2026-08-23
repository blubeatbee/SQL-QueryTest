using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Source.Models;

namespace Source.Data
{
	public partial class GymnasiumDbContext : DbContext
	{
		public GymnasiumDbContext()
		{
		}

		public GymnasiumDbContext(DbContextOptions<GymnasiumDbContext> options)
			: base(options)
		{
		}

		public virtual DbSet<Administrator> Administrators { get; set; }
		public virtual DbSet<Class> Classes { get; set; }
		public virtual DbSet<ClassTeacher> ClassTeachers { get; set; }
		public virtual DbSet<Course> Courses { get; set; }
		public virtual DbSet<CourseClass> CourseClasses { get; set; }
		public virtual DbSet<Employee> Employees { get; set; }
		public virtual DbSet<Grading> Gradings { get; set; }
		public virtual DbSet<Human> Humans { get; set; }
		public virtual DbSet<Principal> Principals { get; set; }
		public virtual DbSet<Student> Students { get; set; }
		public virtual DbSet<Teacher> Teachers { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				.AddJsonFile("appsettings.json")
				.Build();
			optionsBuilder.UseSqlServer(config.GetConnectionString("GymnasiumConnection")).EnableDetailedErrors();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Administrator>(entity =>
			{
				entity.HasKey(e => e.EmployeeId);

				entity.ToTable("Administrator");

				entity.HasIndex(e => e.EmployeeId, "UQ__Administ__7AD04F1085907FDF").IsUnique();

				entity.Property(e => e.EmployeeId).ValueGeneratedNever();
				entity.Property(e => e.Tasks).HasMaxLength(500);

				entity.HasOne(d => d.Employee).WithOne(p => p.Administrator)
					.HasForeignKey<Administrator>(d => d.EmployeeId)
					.OnDelete(DeleteBehavior.SetNull)
					.HasConstraintName("FK_Administrator_Employee");
			});

			modelBuilder.Entity<Class>(entity =>
			{
				entity.HasKey(e => new { e.CyearId, e.ClassId });

				entity.ToTable("Class");

				entity.Property(e => e.CyearId)
					.HasMaxLength(4)
					.IsFixedLength()
					.HasColumnName("CYearId");
				entity.Property(e => e.ClassId)
					.HasMaxLength(3)
					.IsFixedLength();
			});

			modelBuilder.Entity<ClassTeacher>(entity =>
			{
				entity
					.HasNoKey()
					.ToTable("ClassTeacher");

				entity.Property(e => e.ClassId)
					.HasMaxLength(3)
					.IsFixedLength();
				entity.Property(e => e.CyearId)
					.HasMaxLength(4)
					.IsFixedLength()
					.HasColumnName("CYearId");

				entity.HasOne(d => d.Teacher).WithMany()
					.HasForeignKey(d => d.TeacherId)
					.OnDelete(DeleteBehavior.SetNull)
					.HasConstraintName("FK_ClassTeacher_Teacher");

				entity.HasOne(d => d.Class).WithMany()
					.HasForeignKey(d => new { d.CyearId, d.ClassId })
					.OnDelete(DeleteBehavior.SetNull)
					.HasConstraintName("FK_ClassTeacher_Class");
			});

			modelBuilder.Entity<Course>(entity =>
			{
				entity.ToTable("Course");

				entity.Property(e => e.Content).HasMaxLength(500);
			});

			modelBuilder.Entity<CourseClass>(entity =>
			{
				entity
					.HasNoKey()
					.ToTable("CourseClass");

				entity.Property(e => e.ClassId)
					.HasMaxLength(3)
					.IsFixedLength();
				entity.Property(e => e.CyearId)
					.HasMaxLength(4)
					.IsFixedLength()
					.HasColumnName("CYearId");

				entity.HasOne(d => d.Course).WithMany()
					.HasForeignKey(d => d.CourseId)
					.OnDelete(DeleteBehavior.SetNull)
					.HasConstraintName("FK_CourseClass_Course");

				entity.HasOne(d => d.Class).WithMany()
					.HasForeignKey(d => new { d.CyearId, d.ClassId })
					.OnDelete(DeleteBehavior.SetNull)
					.HasConstraintName("FK_CourseClass_Class");
			});

			modelBuilder.Entity<Employee>(entity =>
			{
				entity.ToTable("Employee");

				entity.HasIndex(e => e.EmployeeId, "UQ__Employee__7AD04F10F6263118").IsUnique();

				entity.Property(e => e.EmployeeId).ValueGeneratedNever();
				entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");

				entity.HasOne(d => d.EmployeeNavigation).WithOne(p => p.Employee)
					.HasForeignKey<Employee>(d => d.EmployeeId)
					.OnDelete(DeleteBehavior.Cascade)
					.HasConstraintName("FK_Employee_Human");
			});

			modelBuilder.Entity<Grading>(entity =>
			{
				entity.ToTable("Grading");

				entity.Property(e => e.Grading1)
					.HasMaxLength(1)
					.IsFixedLength()
					.HasColumnName("Grading");

				entity.HasOne(d => d.Course).WithMany(p => p.Gradings)
					.HasForeignKey(d => d.CourseId)
					.OnDelete(DeleteBehavior.SetNull)
					.HasConstraintName("FK_Grading_Course");

				entity.HasOne(d => d.Student).WithMany(p => p.Gradings)
					.HasForeignKey(d => d.StudentId)
					.OnDelete(DeleteBehavior.SetNull)
					.HasConstraintName("FK_Grading_Student");

				entity.HasOne(d => d.Teacher).WithMany(p => p.Gradings)
					.HasForeignKey(d => d.TeacherId)
					.OnDelete(DeleteBehavior.SetNull)
					.HasConstraintName("FK_Grading_Teacher");
			});

			modelBuilder.Entity<Human>(entity =>
			{
				entity.ToTable("Human");

				entity.Property(e => e.Forname).HasMaxLength(50);
				entity.Property(e => e.Midname).HasMaxLength(50);
				entity.Property(e => e.Ssn)
					.HasMaxLength(12)
					.IsFixedLength()
					.HasColumnName("SSN");
				entity.Property(e => e.Surname).HasMaxLength(50);
			});

			modelBuilder.Entity<Principal>(entity =>
			{
				entity.HasKey(e => e.EmployeeId);

				entity.ToTable("Principal");

				entity.HasIndex(e => e.EmployeeId, "UQ__Principa__7AD04F10BB10ED08").IsUnique();

				entity.Property(e => e.EmployeeId).ValueGeneratedNever();
				entity.Property(e => e.Tasks).HasMaxLength(500);

				entity.HasOne(d => d.Employee).WithOne(p => p.Principal)
					.HasForeignKey<Principal>(d => d.EmployeeId)
					.OnDelete(DeleteBehavior.SetNull)
					.HasConstraintName("FK_Principal_Employee");
			});

			modelBuilder.Entity<Student>(entity =>
			{
				entity.ToTable("Student");

				entity.HasIndex(e => e.StudentId, "UQ__Student__32C52B98619C2805").IsUnique();

				entity.Property(e => e.StudentId).ValueGeneratedNever();
				entity.Property(e => e.ClassId)
					.HasMaxLength(3)
					.IsFixedLength();
				entity.Property(e => e.CyearId)
					.HasMaxLength(4)
					.IsFixedLength()
					.HasColumnName("CYearId");

				entity.HasOne(d => d.StudentNavigation).WithOne(p => p.Student)
					.HasForeignKey<Student>(d => d.StudentId)
					.OnDelete(DeleteBehavior.Cascade)
					.HasConstraintName("FK_Student_Human");

				entity.HasOne(d => d.Class).WithMany(p => p.Students)
					.HasForeignKey(d => new { d.CyearId, d.ClassId })
					.OnDelete(DeleteBehavior.SetNull)
					.HasConstraintName("FK_Student_Class");
			});

			modelBuilder.Entity<Teacher>(entity =>
			{
				entity.HasKey(e => e.EmployeeId);

				entity.ToTable("Teacher");

				entity.HasIndex(e => e.EmployeeId, "UQ__Teacher__7AD04F104795E6C1").IsUnique();

				entity.Property(e => e.EmployeeId).ValueGeneratedNever();
				entity.Property(e => e.Tasks).HasMaxLength(500);

				entity.HasOne(d => d.Employee).WithOne(p => p.Teacher)
					.HasForeignKey<Teacher>(d => d.EmployeeId)
					.OnDelete(DeleteBehavior.SetNull)
					.HasConstraintName("FK_Teacher_Employee");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
