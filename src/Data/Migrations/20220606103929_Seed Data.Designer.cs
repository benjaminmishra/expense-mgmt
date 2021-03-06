// <auto-generated />
using System;
using ExpenseMgmt.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExpenseMgmt.Data.Migrations
{
    [DbContext(typeof(ExpenseMgmtDbContext))]
    [Migration("20220606103929_Seed Data")]
    partial class SeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ExpenseMgmt.Data.Bill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("ExpenseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("IncurredOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Serial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UploadedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("ExpenseMgmt.Data.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FullName = "Jon Doe",
                            IsActive = true,
                            ManagerId = 2,
                            Password = "xyz",
                            RoleId = 1
                        },
                        new
                        {
                            Id = 2,
                            FullName = "Jane Doe",
                            IsActive = true,
                            ManagerId = 0,
                            Password = "abc",
                            RoleId = 2
                        },
                        new
                        {
                            Id = 3,
                            FullName = "Accountant Doe",
                            IsActive = true,
                            ManagerId = 2,
                            Password = "abc",
                            RoleId = 3
                        },
                        new
                        {
                            Id = 4,
                            FullName = "Admin Doe",
                            IsActive = true,
                            ManagerId = 2,
                            Password = "abc",
                            RoleId = 4
                        });
                });

            modelBuilder.Entity("ExpenseMgmt.Data.EmployeeRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EmployeeRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Employee",
                            Name = "Employee"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Manager",
                            Name = "Manager"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Acc",
                            Name = "Accountant"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Admin",
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("ExpenseMgmt.Data.ExceptionLogs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("InnerException")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StackTrace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ExceptionLogs");
                });

            modelBuilder.Entity("ExpenseMgmt.Data.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("ExpenseMgmt.Data.ExpensesHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExpenseId")
                        .HasColumnType("int");

                    b.Property<bool>("IsLatest")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Purpose")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseId");

                    b.HasIndex("StatusId");

                    b.ToTable("ExpensesHistories");
                });

            modelBuilder.Entity("ExpenseMgmt.Data.ExpenseStatusType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ExpenseStatusTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Pending for approval",
                            Name = "Pending for approval"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Approved by manager",
                            Name = "Approved"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Pending with accountant",
                            Name = "Pending to be paid"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Paid by accountant",
                            Name = "Paid"
                        });
                });

            modelBuilder.Entity("ExpenseMgmt.Data.Bill", b =>
                {
                    b.HasOne("ExpenseMgmt.Data.Expense", "Expense")
                        .WithMany("Bills")
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expense");
                });

            modelBuilder.Entity("ExpenseMgmt.Data.Employee", b =>
                {
                    b.HasOne("ExpenseMgmt.Data.EmployeeRole", "EmployeeRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeeRole");
                });

            modelBuilder.Entity("ExpenseMgmt.Data.Expense", b =>
                {
                    b.HasOne("ExpenseMgmt.Data.Employee", "Employee")
                        .WithMany("ExpensesLogged")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("ExpenseMgmt.Data.ExpensesHistory", b =>
                {
                    b.HasOne("ExpenseMgmt.Data.Expense", "Expense")
                        .WithMany("History")
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExpenseMgmt.Data.ExpenseStatusType", "ExpenseStatusType")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expense");

                    b.Navigation("ExpenseStatusType");
                });

            modelBuilder.Entity("ExpenseMgmt.Data.Employee", b =>
                {
                    b.Navigation("ExpensesLogged");
                });

            modelBuilder.Entity("ExpenseMgmt.Data.Expense", b =>
                {
                    b.Navigation("Bills");

                    b.Navigation("History");
                });
#pragma warning restore 612, 618
        }
    }
}
