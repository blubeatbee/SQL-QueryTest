using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Source.Models;

namespace Source.Data;

public partial class Gymnasium2Context : DbContext
{
	public Gymnasium2Context()
	{
	}

	public Gymnasium2Context(DbContextOptions<Gymnasium2Context> options)
		: base(options)
	{
	}

	public virtual DbSet<Class> Classes { get; set; }
	public virtual DbSet<ClassEmployee> ClassEmployees { get; set; }
	public virtual DbSet<Course> Courses { get; set; }
	public virtual DbSet<Employee> Employees { get; set; }
	public virtual DbSet<Role> Eroles { get; set; }
	public virtual DbSet<Grading> Gradings { get; set; }
	public virtual DbSet<Student> Students { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var config = new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				.AddJsonFile("appsettings.json")
				.Build();
		optionsBuilder.UseSqlServer(config.GetConnectionString("GymnasiumConnection2"));
		//optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=Gymnasium2;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Class>(entity =>
		{
			entity.HasKey(e => e.ClassId).HasName("PK_Class_ClassId");

			entity.ToTable("Class");

			entity.HasIndex(e => e.ClassId, "UQ_ClassId").IsUnique();

			entity.Property(e => e.ClassId)
				.HasMaxLength(7)
				.IsFixedLength();
		});

		modelBuilder.Entity<ClassEmployee>(entity =>
		{
			entity
				.HasNoKey()
				.ToTable("ClassEmployee");

			entity.Property(e => e.ClassId)
				.HasMaxLength(7)
				.IsFixedLength();

			entity.HasOne(d => d.Class).WithMany()
				.HasForeignKey(d => d.ClassId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_ClassEmployee_Class");

			entity.HasOne(d => d.Employee).WithMany()
				.HasForeignKey(d => d.EmployeeId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_ClassEmployee_Employee");
		});

		modelBuilder.Entity<Course>(entity =>
		{
			entity.HasKey(e => e.CourseId).HasName("PK_Course_CourseId");

			entity.ToTable("Course");

			entity.Property(e => e.ClassId)
				.HasMaxLength(7)
				.IsFixedLength();
			entity.Property(e => e.Title).HasMaxLength(50);

			entity.HasOne(d => d.Class).WithMany(p => p.Courses)
				.HasForeignKey(d => d.ClassId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_Course_Class");
		});

		modelBuilder.Entity<Employee>(entity =>
		{
			entity.HasKey(e => e.EmployeeId).HasName("PK_Employee_EmployeeId");

			entity.ToTable("Employee");

			entity.Property(e => e.Name).HasMaxLength(50);
			entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");
			entity.Property(e => e.Ssn)
				.HasMaxLength(12)
				.IsFixedLength()
				.HasColumnName("SSN");
			entity.Property(e => e.Surname).HasMaxLength(50);
			entity.Property(e => e.Tasks).HasMaxLength(500);

			entity.HasOne(d => d.Role).WithMany(p => p.Employees)
				.HasForeignKey(d => d.RoleId)
				.HasConstraintName("FK_Employee_ERole");
		});

		modelBuilder.Entity<Role>(entity =>
		{
			entity.HasKey(e => e.RoleId).HasName("PK_ERole_RoleId");

			entity.ToTable("ERole");

			entity.HasIndex(e => e.RoleTitle, "UQ_RoleTitle").IsUnique();

			entity.Property(e => e.RoleTitle).HasMaxLength(50);
		});

		modelBuilder.Entity<Grading>(entity =>
		{
			entity.ToTable("Grading");

			entity.HasOne(d => d.Course).WithMany(p => p.Gradings)
				.HasForeignKey(d => d.CourseId)
				.HasConstraintName("FK_Grading_Course");

			entity.HasOne(d => d.Teacher).WithMany(p => p.Gradings)
				.HasForeignKey(d => d.StudentId)
				.HasConstraintName("FK_Grading_Teacher");

			entity.HasOne(d => d.Student).WithMany(p => p.Gradings)
				.HasForeignKey(d => d.StudentId)
				.HasConstraintName("FK_Grading_Student");
		});

		modelBuilder.Entity<Student>(entity =>
		{
			entity.HasKey(e => e.StudentId).HasName("PK_Student_StudentId");

			entity.ToTable("Student");

			entity.Property(e => e.ClassId)
				.HasMaxLength(7)
				.IsFixedLength();
			entity.Property(e => e.Name).HasMaxLength(50);
			entity.Property(e => e.Ssn)
				.HasMaxLength(12)
				.IsFixedLength()
				.HasColumnName("SSN");
			entity.Property(e => e.Surname).HasMaxLength(50);

			entity.HasOne(d => d.Class).WithMany(p => p.Students)
				.HasForeignKey(d => d.ClassId)
				.HasConstraintName("FK_Student_Class");
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
